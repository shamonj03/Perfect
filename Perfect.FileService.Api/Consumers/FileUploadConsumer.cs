using MassTransit;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;
using Perfect.Messages.Commands;

namespace Perfect.FileService.Api.Consumers
{
    public class FileUploadConsumer : IConsumer<FileUploadCommand>
    {
        private readonly IFileService _fileUploadService;

        public FileUploadConsumer(IFileService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public async Task Consume(ConsumeContext<FileUploadCommand> context)
        {
            var request = context.Message;
            var content = await request.Content.Value;

            var command = new UploadFileCommand(request.FileName, request.Length, content);
            await _fileUploadService.SaveFileAsync(command, context.CancellationToken);
        }
    }
}
