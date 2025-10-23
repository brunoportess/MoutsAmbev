namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
public class GetSalesResponse
{
    public required IEnumerable<SaleListItem> Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
public class SaleListItem
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTimeOffset SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
}