namespace Perfect.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public User User { get; set; }

        public Order(Guid id, User user)
        {
            Id = id;
            User = user;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
