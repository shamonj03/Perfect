using Perfect.FileService.Api.Common.Interfaces;
using Perfect.FileService.Api.Common.Models;
using Perfect.FileService.Api.Endpoints.V1.Files.Requests;
using Perfect.FileService.Api.Endpoints.V1.Files.Routes;
using System.Net.Mime;

namespace Perfect.FileService.Api.Endpoints.V1.Files
{
    public class FilesModule : IModule
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("files")
                .HasApiVersion(1.0);

            group
                .MapPost("/", PostFileEndpoint.Execute)
                .WithTags("Upload File")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces<Envelope>(StatusCodes.Status400BadRequest);

            group
                .MapGet("/", GetFileEndpoint.Execute)
                .WithTags("Download File")
                .Accepts<GetFileRequest>(MediaTypeNames.Application.Json)
                .Produces<Envelope>(StatusCodes.Status400BadRequest);
        }
    }
}
