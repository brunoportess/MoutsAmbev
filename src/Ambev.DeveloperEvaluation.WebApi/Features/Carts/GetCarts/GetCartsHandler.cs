using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

public class GetCartsHandler : IRequestHandler<GetCartsRequest, PaginatedList<CartResponse>>
{
    private readonly ICartRepository _repo;
    private readonly IMapper _mapper;

    public GetCartsHandler(ICartRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CartResponse>> Handle(GetCartsRequest request, CancellationToken ct)
    {
        var page = Math.Max(1, request.Page);
        var size = Math.Clamp(request.Size, 1, 200);

        var (data, total) = await _repo.GetPagedAsync(page, size, request.Order, ct);

        var mapped = _mapper.Map<List<CartResponse>>(data);

        return new PaginatedList<CartResponse>(
            mapped,
            total,
            page,
            size
        );
    }
}
