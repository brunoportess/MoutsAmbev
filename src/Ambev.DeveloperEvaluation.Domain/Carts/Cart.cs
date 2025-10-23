namespace Ambev.DeveloperEvaluation.Domain.Carts;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public List<CartProduct> Products { get; set; } = [];
}

public class CartProduct
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
