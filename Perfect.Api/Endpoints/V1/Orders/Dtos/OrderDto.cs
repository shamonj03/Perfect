namespace Perfect.Api.Endpoints.V1.Orders.Dtos
{
    public record OrderDto(int Id, string Name, string Description, float Price, string User);
}
