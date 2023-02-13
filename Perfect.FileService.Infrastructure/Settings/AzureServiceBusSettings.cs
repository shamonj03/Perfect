namespace Perfect.FileService.Infrastructure.Settings
{
    public class AzureServiceBusSettings
    {
        public const string Section = "AzureServiceBus";

        public string? ConnectionString { get; set; } = string.Empty;
    }
}
