using MediatR;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;
public record GetCartByIdRequest(Guid Id) : IRequest<Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse?>;
