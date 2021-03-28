using System;

namespace Orders.Domain.Contracts
{
    public interface IOrderCreatedEvent
    {
        public Guid OrderId { get; }

        public Guid CustomerId { get; }
    }
}