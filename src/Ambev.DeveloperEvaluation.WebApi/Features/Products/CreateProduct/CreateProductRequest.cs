using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequest : IRequest<ProductResponse>
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required, Range(typeof(decimal), "0.01", "79228162514264337593543950335")] public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
    public double? RatingRate { get; set; }
    public int? RatingCount { get; set; }
}

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
    public double? RatingRate { get; set; }
    public int? RatingCount { get; set; }
}
