using CSharpFunctionalExtensions;
using Perfect.Application.Users.Interfaces;
using Perfect.Domain.Models;

namespace Perfect.Infrastructure.Factories
{
    public class UserFactory : IUserFactory
    {
        public Result<User> Create(int id)
        {
            if (id <= 0)
                return Result.Failure<User>("Id must be a positive number.");

            return new User(id, "Test");
        }
    }
}
