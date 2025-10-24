using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleRequest, CreateSaleResponse>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CreateSaleResponse> Handle(CreateSaleRequest request, CancellationToken ct)
    {
        var sale = new Sale(request.CustomerId, string.Empty);
        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductId, item.ProductTitle, item.Quantity, item.UnitPrice);
        }
        await _repo.AddAsync(sale, ct);
        await _repo.SaveAsync(ct);
        return _mapper.Map<CreateSaleResponse>(sale);
    }
}