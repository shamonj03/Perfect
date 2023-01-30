namespace Perfect.Api.Endpoints.V1.Orders.Dtos
{
    public record OrderDto(Guid Id, string Name, string Description, float Price, DateTime CreatedDate, string User);
}
