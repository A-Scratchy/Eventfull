using System;
using System.Threading.Tasks;
using MassTransit;
using Orders.Domain.Contracts;

namespace Orders.Events
{
    public class Publisher
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public Publisher(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task PublishOrderCreatedEvent(Guid orderId, Guid customerId)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("exchange:submit-order"));

            await endpoint.Send<IOrderCreatedEvent>(new
            {
                OrderId = orderId,
                CustomerId = customerId
            });
        }
    }
}