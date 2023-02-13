using Microsoft.Extensions.Options;
using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.FileService.Models;
using Perfect.AnalyzerService.Application.Settings;
using System.Web;

namespace Perfect.AnalyzerService.Infrastructure.FileService
{
    public class FileServiceClient : IFileServiceClient
    {
        private readonly IOptions<FileServiceSettings> _options;
        private readonly HttpClient _httpClient;

        public FileServiceClient(IOptions<FileServiceSettings> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task<FileModel> GetFileAsync(string fileName, CancellationToken cancellationToken)
        {
            HttpRequestMessage CreateMessage()
            {
                var queryBuilder = HttpUtility.ParseQueryString(string.Empty);
                queryBuilder["FileName"] = fileName;
                return new HttpRequestMessage(HttpMethod.Get, $"{_options.Value.FileEndpoint}?{queryBuilder}");
            }

            var request = await _httpClient.SendAsync(CreateMessage(), cancellationToken);

            var content = await request.Content.ReadAsStringAsync();
            request.EnsureSuccessStatusCode();
            return new FileModel(fileName, content);
        }
    }
}
