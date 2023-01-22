using CSharpFunctionalExtensions;
using MongoDB.Driver;
using Perfect.Infrastructure.Persistence.Entities;
using Perfect.Infrastructure.Persistence.Interfaces;

namespace Perfect.Infrastructure.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private const string CollectionName = "orders";

        private readonly IMongoCollection<OrderDbo> _collection;

        public OrderRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<OrderDbo>(CollectionName);
        }

        public async Task<Maybe<OrderDbo>> GetOrder(Guid id, CancellationToken cancellationToken = default)
        {
            var cursor = await _collection.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);

            if (cursor == null)
                return Maybe.None;

            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
