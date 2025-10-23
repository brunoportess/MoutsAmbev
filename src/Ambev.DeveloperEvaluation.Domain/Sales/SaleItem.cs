namespace Ambev.DeveloperEvaluation.Domain.Sales;

public class SaleItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public string ProductTitle { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public decimal Total { get; private set; }

    private SaleItem() { }

    public SaleItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
        if (quantity > 20) throw new ArgumentException("Max 20 identical items per product");
        ProductId = productId;
        ProductTitle = productTitle ?? string.Empty;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ApplyDiscountsAndTotal();
    }

    private void ApplyDiscountsAndTotal()
    {
        // Rules
        // < 4 items: 0%
        // 4..9     : 10%
        // 10..20   : 20%
        if (Quantity >= 10) DiscountPercent = 0.20m;
        else if (Quantity >= 4) DiscountPercent = 0.10m;
        else DiscountPercent = 0m;

        var gross = UnitPrice * Quantity;
        var discount = gross * DiscountPercent;
        Total = decimal.Round(gross - discount, 2, MidpointRounding.AwayFromZero);
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
        if (quantity > 20) throw new ArgumentException("Max 20 identical items per product");
        Quantity = quantity;
        ApplyDiscountsAndTotal();
    }

    public void UpdateUnitPrice(decimal unitPrice)
    {
        UnitPrice = unitPrice;
        ApplyDiscountsAndTotal();
    }
}
