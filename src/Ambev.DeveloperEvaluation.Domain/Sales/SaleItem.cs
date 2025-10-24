using System;

namespace Ambev.DeveloperEvaluation.Domain.Sales
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductTitle { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public decimal Total { get; private set; }

        protected SaleItem() { }

        public SaleItem(Guid productId, string productTitle, int quantity, decimal unitPrice, decimal discountPercent = 0)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductTitle = productTitle;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPercent = discountPercent;

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            var gross = UnitPrice * Quantity;
            var discount = gross * (DiscountPercent / 100);
            Total = gross - discount;
        }
    }
}
