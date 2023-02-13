namespace Perfect.FileService.Application.Settings
{
    public class BlobStorageSettings
    {
        public const string Section = "BlobStorage";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
