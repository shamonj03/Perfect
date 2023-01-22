using CSharpFunctionalExtensions;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Users.Interfaces;
using Perfect.Domain.Models;
using Perfect.Infrastructure.Persistence.Interfaces;

namespace Perfect.Infrastructure.Factories
{
    public class OrderFactory : IOrderFactory
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserFactory _userFactory;

        public OrderFactory(IOrderRepository orderRepository, IUserFactory userFactory)
        {
            _orderRepository = orderRepository;
            _userFactory = userFactory;
        }

        public Task<Result<Order>> Create(Guid id)
        {
            var currentUserId = Guid.NewGuid();
            var user = _userFactory.Create(currentUserId);

            return _orderRepository
                .GetOrder(id)
                .ToResult("No order found")
                .Check(_ => user)
                .Map(x => new Order(x.Id, user.Value)
                {
                    Id = id,
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price
                });
        }
    }
}
