namespace Perfect.FileService.Infrastructure.Settings
{
    public class BlobStorageSettings
    {
        public const string Section = "BlobStorage";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
