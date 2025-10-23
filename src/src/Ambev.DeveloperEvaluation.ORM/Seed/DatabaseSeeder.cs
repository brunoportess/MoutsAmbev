using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Domain.Sales;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        using var scope = serviceProvider.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<DbContext>();

        if (await ctx.Set<Product>().AnyAsync() || await ctx.Set<Cart>().AnyAsync() || await ctx.Set<Sale>().AnyAsync())
        {
            logger.LogInformation("Database already populated, skipping seeding.");
            return;
        }

        logger.LogInformation("🌱 Seeding database with sample data...");

        var productFaker = new Faker<Product>("en")
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(10, 500), 2))
            .RuleFor(p => p.Description, f => f.Lorem.Sentence(8))
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
            .RuleFor(p => p.RatingRate, f => Math.Round(f.Random.Double(1, 5), 1))
            .RuleFor(p => p.RatingCount, f => f.Random.Int(1, 1000));

        var products = productFaker.Generate(30);
        await ctx.Set<Product>().AddRangeAsync(products);
        await ctx.SaveChangesAsync();

        var cartFaker = new Faker<Cart>("en")
            .RuleFor(c => c.UserId, f => f.Random.Int(1, 20))
            .RuleFor(c => c.Date, f => DateOnly.FromDateTime(f.Date.Past(1)))
            .FinishWith((f, c) =>
            {
                var items = new List<CartProduct>();
                foreach (var prod in f.Random.ListItems(products, f.Random.Int(1, 5)))
                {
                    items.Add(new CartProduct
                    {
                        ProductId = prod.Id,
                        Quantity = f.Random.Int(1, 5)
                    });
                }
                c.Products = items;
            });

        var carts = cartFaker.Generate(5);
        await ctx.Set<Cart>().AddRangeAsync(carts);
        await ctx.SaveChangesAsync();

        var saleFaker = new Faker<Sale>("en")
            .CustomInstantiator(f =>
            {
                var sale = new Sale(
                    f.Commerce.Ean13(),
                    DateTimeOffset.UtcNow.AddDays(-f.Random.Int(1, 30)),
                    Guid.NewGuid(),
                    f.Name.FullName(),
                    f.Address.City());

                var prods = f.Random.ListItems(products, f.Random.Int(2, 8));
                foreach (var p in prods)
                {
                    sale.AddItem(Guid.NewGuid(), p.Title, f.Random.Int(1, 10), p.Price);
                }
                return sale;
            });

        var sales = saleFaker.Generate(5);
        await ctx.Set<Sale>().AddRangeAsync(sales);
        await ctx.SaveChangesAsync();

        logger.LogInformation("✅ Seed complete: {ProductCount} products, {CartCount} carts, {SaleCount} sales created.",
            products.Count, carts.Count, sales.Count);
    }
}
