using MediatR;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
public record DeleteCartRequest(int Id) : IRequest<bool>;
