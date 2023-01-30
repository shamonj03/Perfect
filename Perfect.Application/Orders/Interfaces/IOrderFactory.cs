using CSharpFunctionalExtensions;
using Perfect.Domain.Models;

namespace Perfect.Application.Orders.Interfaces
{
    public interface IOrderFactory
    {
        Task<Result<Order>> InitializeAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result<Order>> CreateAsync(CancellationToken cancellationToken = default);
    }
}
