using CSharpFunctionalExtensions;
using Perfect.Domain.Models;

namespace Perfect.Application.Orders.Interfaces
{
    public interface IOrderFactory
    {
        Task<Result<Order>> Create(Guid id);
    }
}
