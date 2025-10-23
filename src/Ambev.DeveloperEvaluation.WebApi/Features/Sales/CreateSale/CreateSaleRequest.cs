using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest : IRequest<CreateSaleResponse>
{
    [Required] public Guid CustomerId { get; set; }
    [Required] public string Branch { get; set; } = string.Empty;
    [MinLength(1)] public List<CreateSaleItemDto> Items { get; set; } = new();
}
public class CreateSaleItemDto
{
    [Required] public Guid ProductId { get; set; }
    [Required, Range(1,20)] public int Quantity { get; set; }
    [Required, Range(typeof(decimal), "0.01", "79228162514264337593543950335")] public decimal UnitPrice { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
}