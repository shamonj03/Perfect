using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Api.Endpoints.V1.Files.Routes
{
    public static class PostFileEndpoint
    {
        public static async Task<IResult> Execute(
            [FromServices] IValidator<IFormFile> validator,
            [FromServices] IFileService fileUploadService,
            [FromForm(Name = "file")] IFormFile request,
            CancellationToken cancellationToken
        )
        {
            await validator.ValidateAndThrowAsync(request);

            using (var stream = request.OpenReadStream())
            using (var memoryStream = new MemoryStream()) {
                await stream.CopyToAsync(memoryStream);

                var command = new UploadFileCommand(request.FileName, memoryStream.Length, memoryStream.ToArray());
                await fileUploadService.SaveFileAsync(command, cancellationToken);
            }
            return Results.Accepted();
        }
    }
}
