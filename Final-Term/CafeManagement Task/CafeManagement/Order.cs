using System;
using System.Collections.Generic;

namespace CafeManagement
{
    public class OrderItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;
    }

    public class Order
    {
        private readonly List<OrderItem> _items = new List<OrderItem>();

        public int Count => _items.Count;

        public OrderItem this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public void Add(OrderItem item) => _items.Add(item);

        public void RemoveAt(int index) => _items.RemoveAt(index);

        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    }
}