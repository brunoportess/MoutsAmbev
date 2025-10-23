using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns a paginated list of sales.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="size">The number of items per page.</param>
    /// <param name="order">Optional ordering expression (e.g. "date desc").</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A paginated list of sales.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetSalesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10,
        [FromQuery(Name = "_order")] string? order = null,
        CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetSalesRequest
        {
            Page = page,
            Size = size,
            Order = order
        }, ct);

        return OkPaginated<GetSalesResponse>(list);
    }

    /// <summary>
    /// Creates a new sale applying discount rules per item.
    /// </summary>
    /// <param name="request">The sale payload.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The created sale.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateSaleResponse>> Create([FromBody] CreateSaleRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return CreatedAtAction(nameof(Get), new { _page = 1, _size = 10 }, result);
    }
}
