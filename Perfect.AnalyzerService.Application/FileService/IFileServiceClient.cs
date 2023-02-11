using Perfect.AnalyzerService.Application.FileService.Models;

namespace Perfect.AnalyzerService.Application.FileService
{
    public interface IFileServiceClient
    {
        Task<FileModel> GetFileAsync(string fileName, CancellationToken cancellationToken);
    }
}
