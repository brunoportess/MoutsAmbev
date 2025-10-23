using Ambev.DeveloperEvaluation.Domain.Carts;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;
public class GetCartsResponse
{
    public required IEnumerable<Cart> Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}