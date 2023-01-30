using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Perfect.Api.Common.Extensions;
using Perfect.Api.Endpoints.v1.Orders.Requests;
using Perfect.Api.Endpoints.V1.Orders.Dtos;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Orders.Requests;

namespace Perfect.Api.Endpoints.v1.Orders.Routes
{
    public static class GetOrderRoute
    {
        public static async Task<IResult> Execute
            ([FromServices] IOrderService orderService,
            [FromServices] IValidator<GetOrderRequest> validator, 
            [AsParameters] GetOrderRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            return await orderService
                .GetOrderAsync(new GetOrderQuery(request.Id))
                .ToEnvelope(order => 
                    new OrderDto(order.Id, order.Name, order.Description, order.Price, order.CreatedDate, order.User.Name));
        }
    }
}
