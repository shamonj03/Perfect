using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Api.Endpoints.V1.Files.Routes
{
    public static class PostFileRoute
    {
        public static async Task<IResult> Execute(
            [FromServices] IValidator<IFormFile> validator,
            [FromServices] IFileUploadService fileUploadService,
            [FromForm(Name = "file")] IFormFile request,
            CancellationToken cancellationToken
        )
        {
            await validator.ValidateAndThrowAsync(request);

            var command = new UploadFileCommand(request.FileName, request.Length, request.OpenReadStream());
            await fileUploadService.UploadAsync(command, cancellationToken);

            return Results.Accepted();
        }
    }
}
