using Perfect.Api.Common.Interfaces;

namespace Perfect.Api.Endpoints.V2.Orders
{
    public class OrdersModule : IModule
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("orders")
                .HasApiVersion(2.0);

            group.MapGet("/exception", () => { throw new InvalidOperationException("Sample Exception"); })
                .WithTags("Exception");
        }
    }
}
