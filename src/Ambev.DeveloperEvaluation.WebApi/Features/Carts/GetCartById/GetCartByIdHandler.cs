using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

public class GetCartByIdHandler : IRequestHandler<GetCartByIdRequest, Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse?>
{
    private readonly ICartRepository _repo;
    public GetCartByIdHandler(ICartRepository repo) { _repo = repo; }

    public async Task<Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse?> Handle(GetCartByIdRequest request, CancellationToken ct)
    {
        var cart = await _repo.GetAsync(request.Id, ct);
        if (cart is null) return null;
        return new Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            Products = cart.Products.Select(p => new Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartItemDto { ProductId = p.ProductId, Quantity = p.Quantity })
        };
    }
}
