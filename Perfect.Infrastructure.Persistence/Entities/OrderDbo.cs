using MongoDB.Bson.Serialization.Attributes;

namespace Perfect.Infrastructure.Persistence.Entities
{
    public class OrderDbo
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
