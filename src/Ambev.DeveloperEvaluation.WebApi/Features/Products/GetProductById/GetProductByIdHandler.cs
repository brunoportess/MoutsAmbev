using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ProductResponse?>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductResponse?> Handle(GetProductByIdRequest request, CancellationToken ct)
    {
        var entity = await _repo.GetAsync(request.Id, ct);
        if (entity is null)
            return null;

        return _mapper.Map<ProductResponse>(entity);
    }
}
