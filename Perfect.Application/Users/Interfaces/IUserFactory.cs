using CSharpFunctionalExtensions;
using Perfect.Domain.Models;

namespace Perfect.Application.Users.Interfaces
{
    public interface IUserFactory
    {
        Result<User> Create(int id);
    }
}
