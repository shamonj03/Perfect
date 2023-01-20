using Microsoft.AspNetCore.Mvc;
using Perfect.Api.Common.Extensions;
using Perfect.Api.Endpoints.v1.Orders.Requests;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Orders.Queries;

namespace Perfect.Api.Endpoints.v1.Orders.Routes
{
    public static class GetOrderRoute
    {
        public static IResult Execute([FromServices] IOrderService orderService, [AsParameters] GetOrderRequest request)
        {
            return orderService
                .GetOrder(new GetOrderQuery(request.Id))
                .ToEnvelope();
        }
    }
}
