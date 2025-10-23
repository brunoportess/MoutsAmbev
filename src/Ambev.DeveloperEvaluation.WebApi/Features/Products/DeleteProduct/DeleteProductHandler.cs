using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, bool>
{
    private readonly IProductRepository _repo;
    public DeleteProductHandler(IProductRepository repo) { _repo = repo; }

    public async Task<bool> Handle(DeleteProductRequest request, CancellationToken ct)
    {
        var entity = await _repo.GetAsync(request.Id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);
        return true;
    }
}
