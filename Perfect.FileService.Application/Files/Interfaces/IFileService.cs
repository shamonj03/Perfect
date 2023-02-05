using Perfect.FileService.Application.Files.Models;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Application.Files.Interfaces
{
    public interface IFileService
    {
        Task<FileModel> GetFileAsync(GetFileQuery query, CancellationToken cancellationToken);

        Task SaveFileAsync(UploadFileCommand command, CancellationToken cancellationToken);
    }
}
