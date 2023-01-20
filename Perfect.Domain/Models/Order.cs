namespace Perfect.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public User User { get; set; }

        public Order(int id, User user)
        {
            Id = id;
            Name = string.Empty;
            Description = string.Empty;
            User = user;
        }
    }
}
