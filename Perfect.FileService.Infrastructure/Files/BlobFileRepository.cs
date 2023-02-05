using Azure.Storage.Blobs;
using Perfect.FileService.Domain.Models;
using Perfect.FileService.Domain.Services;

namespace Perfect.FileService.Infrastructure.Files
{
    public class BlobFileRepository : IFileRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobFileRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<FileEntity> GetFileAsync(string fileName, CancellationToken cancellationToken)
        {
            var containerClient = await GetBlobContainerClient(cancellationToken);

            var blob = containerClient.GetBlobClient(fileName);

            var exists = await blob.ExistsAsync(cancellationToken);

            if(!exists.Value)
                throw new FileNotFoundException(fileName);

            using (var memoryStream = new MemoryStream())
            {
                await blob.DownloadToAsync(memoryStream, cancellationToken);
                return new FileEntity(fileName, memoryStream.Length, memoryStream.ToArray());
            }
        }

        public async Task SaveFileAsync(FileEntity model, CancellationToken cancellationToken)
        {
            // TODO: Get connection string from app settings
            var containerClient = await GetBlobContainerClient(cancellationToken);

            var blob = containerClient.GetBlobClient(model.FileName);

            using (var memoryStream = new MemoryStream(model.Content))
                await blob.UploadAsync(memoryStream, overwrite: true, cancellationToken: cancellationToken);
        }

        private Task<BlobContainerClient> GetBlobContainerClient(CancellationToken cancellationToken)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("files-container");
            return containerClient
                .CreateIfNotExistsAsync(cancellationToken: cancellationToken)
                .ContinueWith(_ => containerClient);
        }
    }
}
