using System;

namespace Orders.Domain.OrderAggregate
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        private Guid ProductId { get; }
        private int Quantity { get; set; }

        public OrderItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public void AmendOrderItemQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public override string ToString() => $"{ProductId} : {Quantity}";
    }
}