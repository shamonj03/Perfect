using MassTransit;

namespace Perfect.FileService.Api.Consumers.FileUpload.Models
{
    public record FileUploadRequest(string Name, long Length, MessageData<Stream> Content);
}
