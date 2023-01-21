using CSharpFunctionalExtensions;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Orders.Models;
using Perfect.Application.Orders.Queries;

namespace Perfect.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderFactory _orderFactory;

        public OrderService(IOrderFactory orderFactory)
        {
            _orderFactory = orderFactory;
        }

        public Result<OrderModel> GetOrder(GetOrderQuery query)
        {
            var result = _orderFactory.Create(query.Id);

            return result
                .MapTry(order => new OrderModel(order.Id, order.Name, order.Description, order.Price, new OrderUserModel(order.User.Name)));
        }
    }
}
