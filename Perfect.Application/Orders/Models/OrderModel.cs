namespace Perfect.Application.Orders.Models
{
    public record OrderModel(int Id, string Name, string Description, float Price, OrderUserModel User);
}
