using Azure.Storage.Blobs;
using Perfect.FileService.Application.Files.Interfaces;

namespace Perfect.FileService.Infrastructure.Files
{
    public class BlobFileRepository : IFileRepository
    {
        public async Task AddFileAsync(string fileName, long length, Stream content, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Get connection string from app settings
                var client = new BlobContainerClient(
                    "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://host.docker.internal:10000/devstoreaccount1;", "files-container"
                );
                await client.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

                using (content)
                {
                    var response = await client.UploadBlobAsync(fileName, content, cancellationToken);
                }
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
