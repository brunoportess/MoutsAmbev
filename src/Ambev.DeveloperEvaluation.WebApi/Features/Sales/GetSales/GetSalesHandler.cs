using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesHandler : IRequestHandler<GetSalesRequest, PaginatedList<GetSalesResponse>>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;

    public GetSalesHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetSalesResponse>> Handle(GetSalesRequest request, CancellationToken ct)
    {
        var page = Math.Max(1, request.Page);
        var size = Math.Clamp(request.Size, 1, 200);

        var (data, total) = await _repo.GetPagedAsync(page, size, request.Order, ct);
        var mapped = _mapper.Map<List<GetSalesResponse>>(data);

        return new PaginatedList<GetSalesResponse>(
            mapped,
            total,
            page,
            size
        );
    }
}
