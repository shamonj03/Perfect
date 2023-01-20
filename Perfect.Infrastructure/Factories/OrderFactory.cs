using CSharpFunctionalExtensions;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Users.Interfaces;
using Perfect.Domain.Models;

namespace Perfect.Infrastructure.Factories
{
    public class OrderFactory : IOrderFactory
    {
        private readonly IUserFactory _userFactory;

        public OrderFactory(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public Result<Order> Create(int id)
        {
            if(id <= 0)
                return Result.Failure<Order>("Id must be a positive number.");

            // TODO: Get user id from subclaim or something.
            var currentUserId = 1;
            var user = _userFactory.Create(currentUserId);

            // TODO: Query orders on user id and order id.
            return user
                .Map(user => new Order(id, user));
        }
    }
}
