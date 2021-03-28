using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Application.Queries;
using Orders.Domain.Aggregates;
using Orders.Infrastructure;

namespace Orders.Application.Handlers
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IList<Order>>
    {
        private readonly IOrdersContext _context;

        public GetOrdersQueryHandler(IOrdersContext context)
        {
            _context = context;
        }

        public async Task<IList<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}