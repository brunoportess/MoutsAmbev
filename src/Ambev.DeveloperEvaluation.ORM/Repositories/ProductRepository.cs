using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _ctx;
    private readonly DbSet<Product> _set;
    public ProductRepository(DbContext ctx)
    {
        _ctx = ctx;
        _set = _ctx.Set<Product>();
    }

    public async Task<(IReadOnlyList<Product> data, int total)> GetPagedAsync(int page, int size, string? order, string? category, CancellationToken ct)
    {
        var q = _set.AsQueryable();
        if (!string.IsNullOrWhiteSpace(category)) q = q.Where(x => x.Category == category);
        if (!string.IsNullOrWhiteSpace(order))
        {
            foreach (var seg in order.Split(','))
            {
                var p = seg.Trim().Split(' ');
                var col = p[0].ToLowerInvariant();
                var desc = p.Length > 1 && p[1].Equals("desc", StringComparison.OrdinalIgnoreCase);
                q = col switch
                {
                    "price" => desc ? q.OrderByDescending(x => x.Price) : q.OrderBy(x => x.Price),
                    "title" => desc ? q.OrderByDescending(x => x.Title) : q.OrderBy(x => x.Title),
                    _ => q
                };
            }
        }
        var total = await q.CountAsync(ct);
        var data = await q.Skip((page-1)*size).Take(size).ToListAsync(ct);
        return (data, total);
    }

    public async Task<IReadOnlyList<string>> GetCategoriesAsync(CancellationToken ct)
        => await _set.Select(x => x.Category).Distinct().OrderBy(x => x).ToListAsync(ct);

    public Task<Product?> GetAsync(Guid id, CancellationToken ct) => _set.FindAsync(new object[]{id}, ct).AsTask();

    public async Task<Product> AddAsync(Product p, CancellationToken ct)
    {
        await _set.AddAsync(p, ct);
        return p;
    }

    public Task<Product> UpdateAsync(Product p, CancellationToken ct)
    {
        _set.Update(p);
        return Task.FromResult(p);
    }

    public Task DeleteAsync(Product p, CancellationToken ct)
    {
        _set.Remove(p);
        return Task.CompletedTask;
    }

    public Task SaveAsync(CancellationToken ct) => _ctx.SaveChangesAsync(ct);
    public Task SaveChangesAsync(CancellationToken ct) => _ctx.SaveChangesAsync(ct);
}
