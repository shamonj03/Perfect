using Perfect.Api.Common.Interfaces;
using Perfect.Api.Common.Models;
using Perfect.Api.Endpoints.v1.Orders.Routes;
using Perfect.Application.Orders.Models;

namespace Perfect.Api.Endpoints.V1.Orders
{
    public class OrdersModule : IModule
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("orders")
                .HasApiVersion(1.0);

            group
                .MapGet("/{id:int}", GetOrderRoute.Execute)
                .WithTags("Get Order")
                .Produces<OrderModel>(StatusCodes.Status200OK)
                .Produces<Envelope>(StatusCodes.Status400BadRequest);
        }
    }
}
