using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DbContext _ctx;
    private readonly DbSet<Cart> _set;

    public CartRepository(DbContext ctx)
    {
        _ctx = ctx;
        _set = _ctx.Set<Cart>();
    }

    public async Task<(IEnumerable<Cart> Data, int Total)> GetPagedAsync(int page, int size, string? order, CancellationToken ct)
    {
        var q = _set.Include(c => c.Products).AsQueryable();

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
                    "userid" => desc ? q.OrderByDescending(x => x.UserId) : q.OrderBy(x => x.UserId),
                    _ => q
                };
            }
        }

        var total = await q.CountAsync(ct);
        var data = await q.Skip((page - 1) * size).Take(size).ToListAsync(ct);

        return (data, total);
    }

    public async Task<Cart?> GetAsync(Guid id, CancellationToken ct)
        => await _set.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<Cart> AddAsync(Cart p, CancellationToken ct)
    {
        await _set.AddAsync(p, ct);
        return p;
    }

    public Task<Cart> UpdateAsync(Cart p, CancellationToken ct)
    {
        _set.Update(p);
        return Task.FromResult(p);
    }

    public Task DeleteAsync(Cart p, CancellationToken ct)
    {
        _set.Remove(p);
        return Task.CompletedTask;
    }

    public Task SaveAsync(CancellationToken ct) => _ctx.SaveChangesAsync(ct);
}
