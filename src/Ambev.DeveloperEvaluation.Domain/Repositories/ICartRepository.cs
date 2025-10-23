using Ambev.DeveloperEvaluation.Domain.Carts;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    //Task<(IReadOnlyList<Cart> data, int total)> GetPagedAsync(int page, int size, string? order, CancellationToken ct);
    Task<(IEnumerable<Cart> Data, int Total)> GetPagedAsync(
        int page,
        int size,
        string? order,
        CancellationToken ct = default);
    Task<Cart?> GetAsync(int id, CancellationToken ct);
    Task<Cart> AddAsync(Cart p, CancellationToken ct);
    Task<Cart> UpdateAsync(Cart p, CancellationToken ct);
    Task DeleteAsync(Cart p, CancellationToken ct);
    Task SaveAsync(CancellationToken ct);
}
