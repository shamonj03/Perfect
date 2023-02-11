using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.FileService.Models;
using System.Net.Http.Json;
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
                var uriBuilder = new UriBuilder("api/v1/files");
                var queryBuilder = HttpUtility.ParseQueryString(string.Empty);
                queryBuilder["FileName"] = fileName;
                uriBuilder.Query = queryBuilder.ToString();
                return new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
            }

            var request = await _httpClient.SendAsync(CreateMessage(), cancellationToken);
            request.EnsureSuccessStatusCode();
            return (await request.Content.ReadFromJsonAsync<FileModel>(cancellationToken: cancellationToken))!;
        }
    }
}
