namespace Perfect.FileService.Application.Files.Requests
{
    public record UploadFileCommand(string FileName, long Length, byte[] Content);
}
