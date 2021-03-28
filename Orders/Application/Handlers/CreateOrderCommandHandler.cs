using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orders.Application.Commands;
using Orders.Domain.Aggregates;
using Orders.Domain.OrderAggregate;
using Orders.Infrastructure;

namespace Orders.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrdersContext _context;

        public CreateOrderCommandHandler(IOrdersContext context) => _context = context;

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new Order(request.CustomerId, request.Description, new Address(request.BillingStreet, request.BillingCity,
                request.BillingState, request.BillingCountry,
                request.BillingPostCode));

            foreach (var item in request.OrderItems)
            {
                entity.AddOrderItem(new OrderItem(item.ProductId, item.Quantity));
            }

            await _context.Orders.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}