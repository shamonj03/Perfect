using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Perfect.Api.Common.Extensions;
using Perfect.Api.Endpoints.v1.Orders.Requests;
using Perfect.Api.Endpoints.V1.Orders.Dtos;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Orders.Queries;

namespace Perfect.Api.Endpoints.v1.Orders.Routes
{
    public static class GetOrderRoute
    {
        public static async Task<Microsoft.AspNetCore.Http.IResult> Execute
            ([FromServices] IOrderService orderService,
            [FromServices] IValidator<GetOrderRequest> validator, 
            [AsParameters] GetOrderRequest request)
        {
            await validator.ValidateAndThrowAsync(request);

            return orderService
                .GetOrder(new GetOrderQuery(request.Id))
                .Map(order => new OrderDto(order.Id, order.Name, order.Description, order.Price, order.User.Name))
                .ToEnvelope();
        }
    }
}
