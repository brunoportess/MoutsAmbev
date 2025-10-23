using Ambev.DeveloperEvaluation.Domain.Products;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
public class GetProductsResponse
{
    public required IEnumerable<Product> Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}