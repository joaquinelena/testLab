using AutoMapper;
using JoaquinOrder.CQRS.Commands;
using JoaquinOrder.CQRS.Queries;
using JoaquinOrder.DTOs;
using JoaquinOrder.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery { Id = id });

            if (order == null)
            {
                return NotFound();
            }

            return _mapper.Map<OrderDTO>(order);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery { CustomerId = customerId });
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(CreateOrderDTO orderDto)
        {
            var command = new CreateOrderCommand
            {
                CustomerId = orderDto.CustomerId,
                Items = orderDto.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            try
            {
                var createdOrder = await _mediator.Send(command);
                return CreatedAtAction("GetOrder", new { id = createdOrder.Id }, _mapper.Map<OrderDTO>(createdOrder));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand { Id = id });
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}