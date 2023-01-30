using Perfect.FileService.Api.Common.Interfaces;
using Perfect.FileService.Api.Common.Models;
using Perfect.FileService.Api.Endpoints.V1.Files.Routes;

namespace Perfect.FileService.Api.Endpoints.V1.Files
{
    public class FilesModule : IModule
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("files")
                .HasApiVersion(1.0);

            group
                .MapPost("/", PostFileRoute.Execute)
                .WithTags("Upload File")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces<Envelope>(StatusCodes.Status400BadRequest);
        }
    }
}
