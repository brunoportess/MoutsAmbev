using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;
public class SalesProfile : Profile
{
    public SalesProfile()
    {
        CreateMap<Sale, CreateSaleResponse>();
        CreateMap<Sale, SaleListItem>();
    }
}