using CSharpFunctionalExtensions;
using Perfect.Domain.Models;

namespace Perfect.Application.Users.Interfaces
{
    public interface IUserFactory
    {
        Task<Result<User>> CreateAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
