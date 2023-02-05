namespace Perfect.FileService.Api.Configuration.Models
{
    public class RabbitMqSettings
    {
        public const string Section = "RabbitMq";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
