using Microsoft.AspNetCore.Mvc;

namespace Perfect.Api.Endpoints.v1.Orders.Requests
{
    public record GetOrderRequest([FromRoute(Name = "id")] int Id);
}
