using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
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

        public async Task<Result<Order>> InitializeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var currentUserId = Guid.NewGuid();
            var user = await _userFactory.CreateAsync(currentUserId, cancellationToken);

            return await _orderRepository
                .GetOrder(id, cancellationToken)
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

        public async Task<Result<Order>> CreateAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = Guid.NewGuid();
            var user = await _userFactory.CreateAsync(currentUserId, cancellationToken);

            return user.Map(x => new Order(Guid.NewGuid(), x));
        }
    }
}
