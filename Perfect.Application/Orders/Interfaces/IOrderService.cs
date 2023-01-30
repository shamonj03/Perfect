using CSharpFunctionalExtensions;
using Perfect.Application.Orders.Models;
using Perfect.Application.Orders.Requests;

namespace Perfect.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<Result<OrderModel>> GetOrderAsync(GetOrderQuery query, CancellationToken cancellationToken = default);
        Task<Result> CreateOrderAsync(CreateOrderCommand command, CancellationToken cancellationToken = default);
    }
}
