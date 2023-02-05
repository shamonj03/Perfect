using Perfect.FileService.Domain.Models;

namespace Perfect.FileService.Domain.Services
{
    public interface IFileRepository
    {
        Task<FileEntity> GetFileAsync(string fileName, CancellationToken cancellationToken);

        Task SaveFileAsync(FileEntity model, CancellationToken cancellationToken);
    }
}
