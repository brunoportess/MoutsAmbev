using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartRequest : IRequest<CartResponse>
{
    [Required] public int UserId { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    [MinLength(1)] public List<CartItemDto> Products { get; set; } = new();
}

public class CartItemDto
{
    [Required] public int ProductId { get; set; }
    [Required, Range(1, 100)] public int Quantity { get; set; }
}

public class CartResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public IEnumerable<CartItemDto> Products { get; set; } = Enumerable.Empty<CartItemDto>();
}
