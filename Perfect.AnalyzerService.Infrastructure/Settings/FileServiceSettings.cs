namespace Perfect.AnalyzerService.Infrastructure.Settings
{
    public class FileServiceSettings
    {
        public const string Section = "FileService";

        public string BaseUrl { get; set; } = string.Empty;

        public string FileEndpoint { get; set; } = string.Empty;
    }
}
