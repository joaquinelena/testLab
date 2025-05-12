using JoaquinOrder.Models;
using JoaquinOrder.Services;
using MediatR;

namespace JoaquinOrder.CQRS.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrderService _orderService;

        public GetOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderService.GetAllOrdersAsync();
        }
    }
}