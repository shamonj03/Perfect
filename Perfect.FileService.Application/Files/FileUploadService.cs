using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Application.Files
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileRepository _fileRepository;

        public FileUploadService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public Task UploadAsync(UploadFileCommand command, CancellationToken cancellationToken)
        {
            var result = _fileRepository.AddFileAsync(command.Name, command.Length, command.Content, cancellationToken);
            
            // TODO: Emit event
            return result;
        }
    }
}
