using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Orders.Domain.Contracts;

namespace Orders.Events
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreatedEvent>
    {
        private readonly ILogger _logger;

        public OrderCreatedConsumer(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
        {
            _logger.Log(LogLevel.Debug, $"Received OrderCreatedEvent : {context.Message.OrderId}");

            //TODO do something with event

            return;
        }
    }
}