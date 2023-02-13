namespace Perfect.SagaService.Infrastructure.Settings
{
    public class MongoSettings
    {
        public const string Section = "Mongo";

        public string? ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
    }
}
