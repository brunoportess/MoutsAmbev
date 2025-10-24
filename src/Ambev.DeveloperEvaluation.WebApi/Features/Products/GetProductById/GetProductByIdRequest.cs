using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;

public record GetProductByIdRequest(Guid Id) : IRequest<ProductResponse>;
