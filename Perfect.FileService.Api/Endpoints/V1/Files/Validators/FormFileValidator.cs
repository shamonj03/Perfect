using FluentValidation;

namespace Perfect.FileService.Api.Endpoints.V1.Files.Validators
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty();

            RuleFor(x => x.Length)
                .NotEmpty();
        }
    }
}