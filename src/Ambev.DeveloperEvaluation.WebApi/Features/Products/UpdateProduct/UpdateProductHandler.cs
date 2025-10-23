using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, ProductResponse?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponse?> Handle(UpdateProductRequest request, CancellationToken ct)
    {
        var entity = await _repository.GetAsync(request.Id, ct);
        if (entity is null)
            return null;

        // Atualiza campos com null-coalescing
        entity.Title = request.Title ?? entity.Title;
        entity.Price = request.Price != 0 ? request.Price : entity.Price;
        entity.Description = request.Description ?? entity.Description;
        entity.Category = request.Category ?? entity.Category;
        entity.Image = request.Image ?? entity.Image;
        entity.RatingRate = request.RatingRate ?? entity.RatingRate;
        entity.RatingCount = request.RatingCount ?? entity.RatingCount;

        await _repository.UpdateAsync(entity, ct);
        await _repository.SaveChangesAsync(ct);

        return _mapper.Map<ProductResponse>(entity);
    }
}
