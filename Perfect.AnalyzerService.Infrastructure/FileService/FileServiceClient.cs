using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.FileService.Models;
using System.Net.Http.Json;
using System.Text;
using System.Web;

namespace Perfect.AnalyzerService.Infrastructure.HttpClients
{
    public class FileServiceClient : IFileServiceClient
    {
        private readonly HttpClient _httpClient;

        public FileServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FileModel> GetFileAsync(string fileName, CancellationToken cancellationToken)
        {
            HttpRequestMessage CreateMessage()
            {
                var queryBuilder = HttpUtility.ParseQueryString(string.Empty);
                queryBuilder["FileName"] = fileName;
                return new HttpRequestMessage(HttpMethod.Get, $"api/v1/files?{queryBuilder}");
            }

            var request = await _httpClient.SendAsync(CreateMessage(), cancellationToken);

            var content = await request.Content.ReadAsStringAsync();
            request.EnsureSuccessStatusCode();
            return new FileModel(fileName, content);
        }
    }
}
