using CSharpFunctionalExtensions;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Orders.Models;
using Perfect.Application.Orders.Requests;

namespace Perfect.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderFactory _orderFactory;

        public OrderService(IOrderFactory orderFactory)
        {
            _orderFactory = orderFactory;
        }

        public async Task<Result<OrderModel>> GetOrderAsync(GetOrderQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _orderFactory.InitializeAsync(query.Id, cancellationToken);

            return result
                .MapTry(order => new OrderModel(order.Id, order.Name, order.Description, order.Price, order.CreatedDate, new OrderUserModel(order.User.Name)));
        }

        public async Task<Result> CreateOrderAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _orderFactory.CreateAsync(cancellationToken);

            return result.Tap(x =>
            {
                x.Name = command.Name;
                x.Description = command.Description;
            });
        }
    }
}
