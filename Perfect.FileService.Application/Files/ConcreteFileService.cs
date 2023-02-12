using Perfect.FileService.Application.Common;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Models;
using Perfect.FileService.Application.Files.Requests;
using Perfect.FileService.Domain.Services;
using Perfect.Messages.Events;

namespace Perfect.FileService.Application.Files
{
    // Avoid namespace collision
    public class ConcreteFileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMessageSender _messageSender;

        public ConcreteFileService(IFileRepository fileRepository, IMessageSender messageSender)
        {
            _fileRepository = fileRepository;
            _messageSender = messageSender;
        }

        public async Task<FileModel> GetFileAsync(GetFileQuery query, CancellationToken cancellationToken)
        {
            var result = await _fileRepository.GetFileAsync(query.FileName, cancellationToken);

            return new FileModel(result.FileName, result.Length, result.Content);
        }

        public async Task SaveFileAsync(UploadFileCommand command, CancellationToken cancellationToken)
        {
            await _fileRepository.SaveFileAsync(new (command.FileName, command.Length, command.Content), cancellationToken);
            await _messageSender.PublishEventAsync(new FileReceivedEvent
            {
                FileId = Guid.NewGuid(),
                FileName = command.FileName,
            }, cancellationToken);
        }
    }
}
