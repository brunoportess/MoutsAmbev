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

        private readonly List<SaleItem> _items = [];
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        protected Sale() { }

        public Sale(Guid id, Guid customerId, string customerName, DateTime saleDate, decimal totalAmount)
        {
            Id = id;
            CustomerId = customerId;
            CustomerName = customerName;
            SaleDate = saleDate;
            TotalAmount = totalAmount;
        }

        public Sale(Guid customerId, string customerName)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CustomerName = customerName;
            SaleDate = DateTime.UtcNow;
        }

        public void AddItem(Guid productId, string productTitle, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(productId, productTitle, quantity, unitPrice);
            _items.Add(item);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            decimal total = 0;
            foreach (var item in _items)
                total += item.Total;

            TotalAmount = total;
        }
    }
}
