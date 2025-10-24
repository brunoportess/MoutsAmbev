using Ambev.DeveloperEvaluation.Domain.Sales;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Sales;

public class SaleItemDiscountTests
{
    [Theory]
    [InlineData(1, 100, 0.00)]
    [InlineData(3, 100, 0.00)]
    [InlineData(4, 100, 0.10)]
    [InlineData(9, 100, 0.10)]
    [InlineData(10, 100, 0.20)]
    [InlineData(20, 100, 0.20)]
    public void DiscountPercent_ByQuantity_IsCorrect(int qty, decimal price, decimal expected)
    {
        var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Test", qty, price);
        Assert.Equal((decimal)expected, item.DiscountPercent);
    }

    [Fact]
    public void Quantity_Above20_Throws()
    {
        Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Test", 21, 100));
    }
}
