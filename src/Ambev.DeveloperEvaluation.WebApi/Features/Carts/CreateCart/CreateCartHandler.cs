using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartRequest, CartResponse>
{
    private readonly ICartRepository _repo;
    private readonly IMapper _mapper;
    public CreateCartHandler(ICartRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CartResponse> Handle(CreateCartRequest request, CancellationToken ct)
    {
        var cart = new Cart
        {
            UserId = request.UserId,
            Date = request.Date,     
            Products = request.Products.Select(p => new CartProduct { ProductId = p.ProductId, Quantity = p.Quantity }).ToList()
        };
        await _repo.AddAsync(cart, ct);
        await _repo.SaveAsync(ct);
        return new CartResponse
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            Products = cart.Products.Select(p => new CartItemDto { ProductId = p.ProductId, Quantity = p.Quantity })
        };
    }
}
