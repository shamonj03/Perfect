namespace Perfect.AnalyzerService.Application.Settings
{
    public class RabbitMqSettings
    {
        public const string Section = "RabbitMq";

        public string? ConnectionString { get; set; } = string.Empty;

        public string AnalyzeFileCommandQueue { get; set; } = string.Empty;
    }
}
