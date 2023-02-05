using FluentValidation;
using Perfect.FileService.Api.Endpoints.V1.Files.Requests;

namespace Perfect.FileService.Api.Endpoints.V1.Files.Validators
{
    public class GetFileRequestValidator : AbstractValidator<GetFileRequest>
    {
        public GetFileRequestValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty();
        }
    }
}
