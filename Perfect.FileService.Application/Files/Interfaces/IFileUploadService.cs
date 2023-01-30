using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Application.Files.Interfaces
{
    public interface IFileUploadService
    {
        Task UploadAsync(UploadFileCommand command, CancellationToken cancellationToken);
    }
}
