using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartRequest, bool>
{
    private readonly ICartRepository _repo;
    public DeleteCartHandler(ICartRepository repo) { _repo = repo; }

    public async Task<bool> Handle(DeleteCartRequest request, CancellationToken ct)
    {
        var entity = await _repo.GetAsync(request.Id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        await _repo.SaveAsync(ct);
        return true;
    }
}
