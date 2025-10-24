using MediatR;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
public record DeleteCartRequest(Guid Id) : IRequest<bool>;
