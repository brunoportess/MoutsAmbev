using Ambev.DeveloperEvaluation.Domain.Products;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<(IReadOnlyList<Product> data, int total)> GetPagedAsync(int page, int size, string? order, string? category, CancellationToken ct);
    Task<IReadOnlyList<string>> GetCategoriesAsync(CancellationToken ct);
    Task<Product?> GetAsync(int id, CancellationToken ct);
    Task<Product> AddAsync(Product p, CancellationToken ct);
    Task<Product> UpdateAsync(Product p, CancellationToken ct);
    Task DeleteAsync(Product p, CancellationToken ct);
    Task SaveAsync(CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct = default);

}
