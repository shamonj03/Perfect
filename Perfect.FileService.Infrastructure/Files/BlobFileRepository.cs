using Azure.Storage.Blobs;
using Perfect.FileService.Application.Files.Interfaces;

namespace Perfect.FileService.Infrastructure.Files
{
    public class BlobFileRepository : IFileRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobFileRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task AddFileAsync(string fileName, long length, Stream content, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Get connection string from app settings
                var containerClient = _blobServiceClient.GetBlobContainerClient("files-container");
                await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

                using (content)
                {
                    var blob = containerClient.GetBlobClient(fileName);
                    await blob.UploadAsync(content, overwrite: true, cancellationToken: cancellationToken);
                }
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
