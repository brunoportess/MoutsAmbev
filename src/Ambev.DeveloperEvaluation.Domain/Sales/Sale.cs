namespace Ambev.DeveloperEvaluation.Domain.Sales;

public class Sale
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string SaleNumber { get; private set; } = string.Empty;
    public DateTimeOffset SaleDate { get; private set; } = DateTimeOffset.UtcNow;
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; } = string.Empty;
    public string Branch { get; private set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    private readonly List<SaleItem> _items = [];
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    private Sale() { }

    public Sale(string saleNumber, DateTimeOffset saleDate, Guid customerId, string customerName, string branch)
    {
        if (string.IsNullOrWhiteSpace(saleNumber)) throw new ArgumentException("SaleNumber required");
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        CustomerId = customerId;
        CustomerName = customerName ?? string.Empty;
        Branch = branch ?? string.Empty;
    }

    public void AddItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
    {
        var item = new SaleItem(productId, productTitle, quantity, unitPrice);
        _items.Add(item);
        RecalculateTotal();
    }

    public void RemoveItem(Guid productId)
    {
        var toRemove = _items.FirstOrDefault(i => i.ProductId == productId);
        if (toRemove is null) return;
        _items.Remove(toRemove);
        RecalculateTotal();
    }

    public void Cancel() => IsCancelled = true;

    public void RecalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.Total);
    }
}
