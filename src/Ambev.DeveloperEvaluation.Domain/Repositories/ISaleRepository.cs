using Ambev.DeveloperEvaluation.Domain.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<(IEnumerable<Sale> Data, int Total)> GetPagedAsync(
        int page,
        int size,
        string? order,
        CancellationToken ct = default);

    Task<Sale?> GetAsync(Guid id, CancellationToken ct);
    Task<Sale> AddAsync(Sale sale, CancellationToken ct);
    Task<Sale> UpdateAsync(Sale sale, CancellationToken ct);
    Task DeleteAsync(Sale sale, CancellationToken ct);
    Task SaveAsync(CancellationToken ct);
}
