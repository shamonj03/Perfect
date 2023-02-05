namespace Perfect.FileService.Application.Files.Models
{
    public record FileModel(string FileName, long Length, byte[] Content);
}
