namespace Perfect.AnalyzerService.Api.Configuration.Models
{
    public class AzureServiceBusSettings
    {
        public const string Section = "AzureServiceBus";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
