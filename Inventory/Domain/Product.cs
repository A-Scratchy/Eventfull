using System;

namespace Inventory.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public Uri ImageUri { get; set; }
    }

    public enum Category
    {
        Physical,
        EventTicket,
        Service
    }
}