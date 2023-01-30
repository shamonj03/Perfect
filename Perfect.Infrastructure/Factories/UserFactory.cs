using CSharpFunctionalExtensions;
using Perfect.Application.Users.Interfaces;
using Perfect.Domain.Models;

namespace Perfect.Infrastructure.Factories
{
    public class UserFactory : IUserFactory
    {
        public Task<Result<User>> CreateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                return Task.FromResult(Result.Failure<User>("Id must not be empty."));

            return Task.FromResult<Result<User>>(new User(id, "Test"));
        }
    }
}
