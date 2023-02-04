namespace Perfect.FileService.Api.Configuration.Models
{
    public class BlobStorageSettings
    {
        public const string Section = "BlobStorage";

        public string ConnectionString { get; set; }
    }
}
