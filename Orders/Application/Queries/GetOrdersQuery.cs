using System.Collections.Generic;
using MediatR;
using Orders.Domain.Aggregates;

namespace Orders.Application.Queries
{
    public class GetOrdersQuery: IRequest<IList<Order>>
    {
        
    }
}