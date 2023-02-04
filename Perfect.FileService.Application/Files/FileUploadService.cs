using Perfect.FileService.Application.Common;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;
using Perfect.Messages.Events;

namespace Perfect.FileService.Application.Files
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMessageSender _messageSender;

        public FileUploadService(IFileRepository fileRepository, IMessageSender messageSender)
        {
            _fileRepository = fileRepository;
            _messageSender = messageSender;
        }

        public async Task UploadAsync(UploadFileCommand command, CancellationToken cancellationToken)
        {
            await _fileRepository.AddFileAsync(command.FileName, command.Length, command.Content, cancellationToken);
            await _messageSender.PublishEventAsync(new FileReceivedEvent
            {
                FileName = command.FileName,
            }, cancellationToken);
        }
    }
}
