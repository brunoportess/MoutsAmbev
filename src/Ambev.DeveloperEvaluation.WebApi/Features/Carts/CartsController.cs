using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Returns a paginated list of carts with their products.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="order">Optional ordering expression (e.g. "date desc").</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A paginated list of carts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<CartResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10,
        [FromQuery(Name = "_order")] string? order = null,
        CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetCartsRequest
        {
            Page = page,
            Size = size,
            Order = order
        }, ct);

        return OkPaginated<CartResponse>(list);
    }

    /// <summary>
    /// Creates a new cart with products.
    /// </summary>
    /// <param name="request">The cart payload including user and products.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created cart.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CartResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CartResponse>> Create(
        [FromBody] CreateCartRequest request,
        CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets a cart by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The cart if found, otherwise 404.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCartById.GetCartByIdRequest(id), ct);
        if (result is null) return (ActionResult)NotFound();
        return (ActionResult)Ok(result);
    }

    /// <summary>
    /// Updates an existing cart and its products.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to update.</param>
    /// <param name="request">The payload containing updated cart data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The updated cart or 404 if not found.</returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartResponse>> Update(
        Guid id,
        [FromBody] UpdateCart.UpdateCartRequest request,
        CancellationToken ct)
    {
        request.Id = id;
        var result = await _mediator.Send(request, ct);
        if (result is null) return (ActionResult)NotFound();
        return (ActionResult)Ok(result);
    }

    /// <summary>
    /// Deletes a cart by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>No content response if the cart was deleted successfully, or 404 if not found.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await _mediator.Send(new DeleteCart.DeleteCartRequest(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
