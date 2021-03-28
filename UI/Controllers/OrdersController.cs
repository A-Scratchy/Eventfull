using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Commands;
using Orders.Application.Queries;
using Orders.Domain.Aggregates;
using UI.Pages;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _mediator;

        public OrdersController(ISender mediator)
        {
            _mediator = mediator;
        }

        public class createOrderBody
        {
            public string Description { get; set; } 
        }
        
        [HttpPost]
        public void Create([FromBody]createOrderBody data)
        {
            var command = new CreateOrderCommand
            {
               Description = data.Description
            };
            
            _mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<Order>> Get()
        {
           var query = new GetOrdersQuery();
           var response =  await _mediator.Send(query);
           return response.ToList();
        }
        
    }
}