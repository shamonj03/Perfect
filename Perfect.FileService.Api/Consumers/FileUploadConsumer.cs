using MassTransit;
using Perfect.FileService.Api.Consumers.FileUpload.Models;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Api.Consumers
{
    public class FileUploadConsumer : IConsumer<FileUploadCommand>
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadConsumer(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public async Task Consume(ConsumeContext<FileUploadCommand> context)
        {
            var request = context.Message;
            var content = await request.Content.Value;

            var command = new UploadFileCommand(request.FileName, request.Length, content);
            await _fileUploadService.UploadAsync(command, context.CancellationToken);
        }
    }
}
