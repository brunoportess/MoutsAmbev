namespace Ambev.DeveloperEvaluation.Domain.Carts;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public List<CartProduct> Products { get; set; } = [];
}

public class CartProduct
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
