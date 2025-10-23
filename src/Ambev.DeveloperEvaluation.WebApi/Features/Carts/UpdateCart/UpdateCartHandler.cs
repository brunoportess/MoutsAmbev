using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Carts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartRequest, Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse?>
{
    private readonly ICartRepository _repo;
    public UpdateCartHandler(ICartRepository repo) { _repo = repo; }

    public async Task<Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse?> Handle(UpdateCartRequest request, CancellationToken ct)
    {
        var cart = await _repo.GetAsync(request.Id, ct);
        if (cart is null) return null;

        cart.UserId = request.UserId;
        cart.Date = request.Date;
        cart.Products = request.Products.Select(p => new CartProduct { ProductId = p.ProductId, Quantity = p.Quantity }).ToList();

        await _repo.UpdateAsync(cart, ct);
        await _repo.SaveAsync(ct);

        return new Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            Products = cart.Products.Select(p => new Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartItemDto { ProductId = p.ProductId, Quantity = p.Quantity })
        };
    }
}
