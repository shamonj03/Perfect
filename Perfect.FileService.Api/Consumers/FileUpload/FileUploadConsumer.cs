using MassTransit;
using Perfect.FileService.Api.Consumers.FileUpload.Models;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Application.Files.Requests;

namespace Perfect.FileService.Api.Consumers.FileUpload
{
    public class FileUploadConsumer : IConsumer<FileUploadRequest>
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadConsumer(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public async Task Consume(ConsumeContext<FileUploadRequest> context)
        {
            var request = context.Message;
            var content = await request.Content.Value;

            var command = new UploadFileCommand(request.Name, request.Length, content);
            await _fileUploadService.UploadAsync(command, context.CancellationToken);
        }
    }
}
