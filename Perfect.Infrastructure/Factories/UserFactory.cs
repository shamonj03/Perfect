using CSharpFunctionalExtensions;
using Perfect.Application.Users.Interfaces;
using Perfect.Domain.Models;

namespace Perfect.Infrastructure.Factories
{
    public class UserFactory : IUserFactory
    {
        public Result<User> Create(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Failure<User>("Id must not be empty.");

            return new User(id, "Test");
        }
    }
}
