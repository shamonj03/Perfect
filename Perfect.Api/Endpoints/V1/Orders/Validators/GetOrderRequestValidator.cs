using FluentValidation;
using Perfect.Api.Endpoints.v1.Orders.Requests;

namespace Perfect.Api.Endpoints.V1.Orders.Validators
{
    public class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
    {
        public GetOrderRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode(StatusCodes.Status404NotFound.ToString())
                .WithMessage("Id must be greater than zero.");
        }
    }
}
