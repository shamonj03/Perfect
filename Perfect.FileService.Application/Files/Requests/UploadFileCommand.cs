namespace Perfect.FileService.Application.Files.Requests
{
    public record UploadFileCommand(string Name, long Length, Stream Content);
}
