using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Returns products with pagination, ordering, and optional category filter.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="order">Optional ordering expression (e.g. "price desc").</param>
    /// <param name="category">Optional category filter.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A paginated list of products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct.ProductResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10,
        [FromQuery(Name = "_order")] string? order = null,
        [FromQuery] string? category = null,
        CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetProductsRequest
        {
            Page = page,
            Size = size,
            Order = order,
            Category = category
        }, ct);

        return OkPaginated<ProductResponse>(list);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product payload containing title, price, description, and category.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created product.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct.ProductResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create(
        [FromBody] Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct.CreateProductRequest request,
        CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets a product by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The product if found, otherwise 404.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct.ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById.GetProductByIdRequest(id), ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The unique identifier of the product to update.</param>
    /// <param name="request">The payload containing updated product information.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The updated product or 404 if not found.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct.ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct.UpdateProductRequest request,
        CancellationToken ct)
    {
        request.Id = id;
        var result = await _mediator.Send(request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>No content response if the product was deleted successfully, or 404 if not found.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var ok = await _mediator.Send(new Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct.DeleteProductRequest(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
