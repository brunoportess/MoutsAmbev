using Ambev.DeveloperEvaluation.Domain.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale?> GetAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Sale>> GetPagedAsync(int page, int size, string? order, CancellationToken ct);
    Task<int> CountAsync(CancellationToken ct);
    Task AddAsync(Sale sale, CancellationToken ct);
    Task UpdateAsync(Sale sale, CancellationToken ct);
    Task DeleteAsync(Sale sale, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
