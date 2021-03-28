using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orders.Domain.OrderAggregate;

namespace Orders.Domain.Aggregates
{
    public class Order : IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }

        private Guid CustomerId { get; set; }
        private DateTime OrderDate { get; set; }

        public Address ShippingAddress { get; set; }

        public Address BillingAddress { get; set; }

        public OrderStatus OrderStatus { get; set; }

        private List<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public Order()
        {
        }

        public Order(Guid customerId, string description, Address billingAddress, Address shippingAddress = null)
        {
            Id = Guid.NewGuid();
            Description = description;
            OrderDate = DateTime.Now;
            CustomerId = customerId;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }

        public void AddOrderItem(Guid productId, int quantity)
        {
            OrderItems.Add(new OrderItem(productId, quantity));
        }

        public override string ToString() => $"Order: {Id}";
    }
}