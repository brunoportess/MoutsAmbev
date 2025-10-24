using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Sales
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public DateTime SaleDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        public List<SaleItem> SaleItems { get; private set; } = [];

        protected Sale() { }

        public Sale(Guid customerId, string customerName)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CustomerName = customerName;
            SaleDate = DateTime.UtcNow;
        }

        public void AddItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(Id, productId, productTitle, quantity, unitPrice);
            SaleItems.Add(item);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in SaleItems)
                TotalAmount += item.Total;
        }
    }
}
