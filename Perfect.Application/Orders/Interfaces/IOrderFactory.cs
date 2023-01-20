using CSharpFunctionalExtensions;
using Perfect.Domain.Models;

namespace Perfect.Application.Orders.Interfaces
{
    public interface IOrderFactory
    {
        Result<Order> Create(int id);
    }
}
