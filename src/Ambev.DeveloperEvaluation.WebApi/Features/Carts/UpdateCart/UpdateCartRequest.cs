using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartRequest : IRequest<Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartResponse?>
{
    [Required] public int Id { get; set; }
    [Required] public int UserId { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    [MinLength(1)] public List<Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CartItemDto> Products { get; set; } = [];
}
