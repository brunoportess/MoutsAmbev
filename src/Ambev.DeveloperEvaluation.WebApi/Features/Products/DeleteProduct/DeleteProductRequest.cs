using MediatR;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
public record DeleteProductRequest(Guid Id) : IRequest<bool>;
