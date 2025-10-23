using MediatR;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
public record DeleteProductRequest(int Id) : IRequest<bool>;
