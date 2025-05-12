using JoaquinOrder.Models;
using JoaquinOrder.Services;
using MediatR;

namespace JoaquinOrder.CQRS.Queries
{
    public class GetOrdersByCustomerIdQuery : IRequest<IEnumerable<Order>>
    {
        public int CustomerId { get; set; }
    }

    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<Order>>
    {
        private readonly IOrderService _orderService;

        public GetOrdersByCustomerIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderService.GetOrdersByCustomerIdAsync(request.CustomerId);
        }
    }
}