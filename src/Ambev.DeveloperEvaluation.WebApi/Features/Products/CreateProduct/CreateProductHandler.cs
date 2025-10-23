using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductResponse>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public CreateProductHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductResponse> Handle(CreateProductRequest request, CancellationToken ct)
    {
        var entity = _mapper.Map<Product>(request);
        await _repo.AddAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);
        return _mapper.Map<ProductResponse>(entity);
    }
}
