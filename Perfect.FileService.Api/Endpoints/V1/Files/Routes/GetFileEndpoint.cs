using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Perfect.FileService.Api.Endpoints.V1.Files.Requests;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Api.Endpoints.V1.Files.Routes
{
    public static class GetFileEndpoint
    {
        public static async Task<IResult> Execute(
            [FromServices] IValidator<GetFileRequest> validator,
            [FromServices] IFileService fileUploadService,
            [AsParameters] GetFileRequest request,
            CancellationToken cancellationToken
        )
        {
            await validator.ValidateAndThrowAsync(request);

            var query = new GetFileQuery(request.FileName);
            var result = await fileUploadService.GetFileAsync(query, cancellationToken);

            return Results.File(result.Content, fileDownloadName: result.FileName);
        }
    }
}
