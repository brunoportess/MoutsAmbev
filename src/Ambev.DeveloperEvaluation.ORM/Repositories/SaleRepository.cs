using Ambev.DeveloperEvaluation.Domain.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DbContext _ctx;
    private readonly DbSet<Sale> _set;

    public SaleRepository(DbContext ctx)
    {
        _ctx = ctx;
        _set = _ctx.Set<Sale>();
    }

    public async Task<(IEnumerable<Sale> Data, int Total)> GetPagedAsync(
        int page,
        int size,
        string? order,
        CancellationToken ct)
    {
        var q = _set.Include(s => s.Items).AsQueryable();

        if (!string.IsNullOrWhiteSpace(order))
        {
            foreach (var seg in order.Split(','))
            {
                var p = seg.Trim().Split(' ');
                var col = p[0].ToLowerInvariant();
                var desc = p.Length > 1 && p[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                q = col switch
                {
                    "id" => desc ? q.OrderByDescending(x => x.Id) : q.OrderBy(x => x.Id),
                    "date" => desc ? q.OrderByDescending(x => x.SaleDate) : q.OrderBy(x => x.SaleDate),
                    "customer" => desc ? q.OrderByDescending(x => x.CustomerName) : q.OrderBy(x => x.CustomerName),
                    _ => q
                };
            }
        }

        var total = await q.CountAsync(ct);
        var data = await q.Skip((page - 1) * size).Take(size).ToListAsync(ct);

        return (data, total);
    }

    public async Task<Sale?> GetAsync(Guid id, CancellationToken ct)
        => await _set.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<Sale> AddAsync(Sale sale, CancellationToken ct)
    {
        await _set.AddAsync(sale, ct);
        return sale;
    }

    public Task<Sale> UpdateAsync(Sale sale, CancellationToken ct)
    {
        _set.Update(sale);
        return Task.FromResult(sale);
    }

    public Task DeleteAsync(Sale sale, CancellationToken ct)
    {
        _set.Remove(sale);
        return Task.CompletedTask;
    }

    public Task SaveAsync(CancellationToken ct)
        => _ctx.SaveChangesAsync(ct);
}
