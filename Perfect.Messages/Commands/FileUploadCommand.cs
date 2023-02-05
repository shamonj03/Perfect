using MassTransit;

namespace Perfect.Messages.Commands
{
    public record FileUploadCommand
    {
        public string FileName { get; init; } = string.Empty;

        public long Length { get; init; }

        public MessageData<byte[]> Content { get; init; } = null!;
    }
}
