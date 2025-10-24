using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequest : IRequest<ProductResponse?>
{
    [Required] 
    public Guid Id { get; set; }
    [Required] 
    public string Title { get; set; } = string.Empty;
    [Required] 
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
    public double? RatingRate { get; set; }
    public int? RatingCount { get; set; }
}
