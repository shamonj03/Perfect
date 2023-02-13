namespace Perfect.SagaService.Infrastructure.Settings
{
    public class RabbitMqSettings
    {
        public const string Section = "RabbitMq";

        public string? ConnectionString { get; set; } = string.Empty;

        public string SagaQueue { get; set; } = string.Empty;
    }
}
