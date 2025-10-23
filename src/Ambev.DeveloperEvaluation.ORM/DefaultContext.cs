using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Ambev.DeveloperEvaluation.Domain.Sales.Sale> Sales { get; set; }
    public DbSet<Ambev.DeveloperEvaluation.Domain.Products.Product> Products { get; set; }
    public DbSet<Ambev.DeveloperEvaluation.Domain.Carts.Cart> Carts { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        

        // Product
        modelBuilder.Entity<Ambev.DeveloperEvaluation.Domain.Products.Product>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Title).HasMaxLength(200).IsRequired();
            b.Property(p => p.Price).HasColumnType("numeric(18,2)");
            b.Property(p => p.Category).HasMaxLength(100);
            b.Property(p => p.RatingRate).HasColumnType("numeric(5,2)");
            b.Property(p => p.RatingCount);
        });

        // Cart
        modelBuilder.Entity<Ambev.DeveloperEvaluation.Domain.Carts.Cart>(b =>
        {
            b.HasKey(c => c.Id);
            b.OwnsMany<Ambev.DeveloperEvaluation.Domain.Carts.CartProduct>("Products", cb =>
            {
                cb.WithOwner().HasForeignKey("CartId");
                cb.Property<int>("Id");
                cb.HasKey("Id");
                cb.Property(i => i.ProductId).IsRequired();
                cb.Property(i => i.Quantity).IsRequired();
            });
        });
        
        // Sale
        modelBuilder.Entity<Ambev.DeveloperEvaluation.Domain.Sales.Sale>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(40);
            builder.Property(s => s.CustomerName).HasMaxLength(200);
            builder.Property(s => s.Branch).HasMaxLength(80);
            builder.Property(s => s.TotalAmount).HasColumnType("numeric(18,2)");
            builder.Navigation("_items").UsePropertyAccessMode(Microsoft.EntityFrameworkCore.PropertyAccessMode.Field);

            builder.OwnsMany<Ambev.DeveloperEvaluation.Domain.Sales.SaleItem>("_items", b =>
            {
                b.WithOwner().HasForeignKey("SaleId");
                b.Property<Guid>("Id").ValueGeneratedNever();
                b.HasKey("Id");
                b.Property(i => i.ProductId).IsRequired();
                b.Property(i => i.ProductTitle).HasMaxLength(200);
                b.Property(i => i.Quantity).IsRequired();
                b.Property(i => i.UnitPrice).HasColumnType("numeric(18,2)");
                b.Property(i => i.DiscountPercent).HasColumnType("numeric(5,2)");
                b.Property(i => i.Total).HasColumnType("numeric(18,2)");
            });
        });

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}