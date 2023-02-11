namespace Perfect.AnalyzerService.Application.FileService.Models
{
    public record FileModel(string FileName, long Length, byte[] Content);
}
