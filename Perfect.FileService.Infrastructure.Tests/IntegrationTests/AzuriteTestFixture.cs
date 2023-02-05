using Azure.Storage.Blobs;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Perfect.FileService.Infrastructure.Files;
using Xunit;

namespace Perfect.FileService.Infrastructure.Tests.IntegrationTests
{
    public class AzuriteTestFixture : IAsyncLifetime
    {
        public readonly BlobServiceClient BlobClient;
        public readonly BlobContainerClient BlobContainer;

        private readonly IContainer _container;

        public AzuriteTestFixture()
        {
            const int Port = 10000;

            BlobClient = new BlobServiceClient(
                $"AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://localhost:{Port}/devstoreaccount1;");

            BlobContainer = BlobClient.GetBlobContainerClient(BlobFileRepository.ContainerName);

            _container = new ContainerBuilder()
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                .WithPortBinding(Port, 10000)
                //.WithCommand($"azurite-blob --blobHost 0.0.0.0 --blobPort 10000")
                .Build();
        }

        public Task InitializeAsync() => _container.StartAsync();

        public async Task DisposeAsync() 
        {
            await _container.StopAsync();
            await _container.DisposeAsync();
        }
    }
}
