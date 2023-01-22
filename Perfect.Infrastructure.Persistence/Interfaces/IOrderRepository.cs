using CSharpFunctionalExtensions;
using Perfect.Infrastructure.Persistence.Entities;

namespace Perfect.Infrastructure.Persistence.Interfaces
{
    public interface IOrderRepository
    {
        Task<Maybe<OrderDbo>> GetOrder(Guid id, CancellationToken cancellationToken = default);
    }
}
