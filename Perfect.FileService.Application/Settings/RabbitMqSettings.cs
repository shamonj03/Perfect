namespace Perfect.FileService.Application.Settings
{
    public class RabbitMqSettings
    {
        public const string Section = "RabbitMq";

        public string? ConnectionString { get; set; } = string.Empty;

        public string FileUploadCommandQueue { get; set; } = string.Empty;
    }
}
