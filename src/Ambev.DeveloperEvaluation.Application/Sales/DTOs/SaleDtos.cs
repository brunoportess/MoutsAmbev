using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs;

public record SaleItemInputDto(
    Guid ProductId,
    string ProductTitle,
    [Range(1, 20)] int Quantity,
    [Range(0, double.MaxValue)] decimal UnitPrice
);

public record CreateSaleRequest(
    string SaleNumber,
    DateTimeOffset SaleDate,
    Guid CustomerId,
    string CustomerName,
    string Branch,
    List<SaleItemInputDto> Items
);

public record UpdateSaleRequest(
    DateTimeOffset SaleDate,
    Guid CustomerId,
    string CustomerName,
    string Branch,
    List<SaleItemInputDto> Items
);

public record SaleItemDto(
    Guid ProductId,
    string ProductTitle,
    int Quantity,
    decimal UnitPrice,
    decimal DiscountPercent,
    decimal Total
);

public record SaleDto(
    Guid Id,
    string SaleNumber,
    DateTimeOffset SaleDate,
    Guid CustomerId,
    string CustomerName,
    string Branch,
    decimal TotalAmount,
    bool IsCancelled,
    List<SaleItemDto> Items
);
