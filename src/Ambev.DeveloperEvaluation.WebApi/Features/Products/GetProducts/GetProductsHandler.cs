using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsRequest, PaginatedList<ProductResponse>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductResponse>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        // Repositório retorna a lista paginada + total
        var (items, totalCount) = await _repository.GetPagedAsync(
            request.Page,
            request.Size,
            request.Order,
            request.Category,
            cancellationToken);

        // Mapeia o domínio -> DTO
        var data = _mapper.Map<List<ProductResponse>>(items);

        // Cria a lista paginada padronizada
        return new PaginatedList<ProductResponse>(
            data,
            totalCount,
            request.Page,
            request.Size
        );
    }
}
