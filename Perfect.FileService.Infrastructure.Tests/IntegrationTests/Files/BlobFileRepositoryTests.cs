using AutoFixture;
using AutoFixture.AutoMoq;
using Azure.Storage.Blobs;
using Perfect.FileService.Domain.Models;
using Perfect.FileService.Infrastructure.Files;
using Shouldly;
using System.Text;
using Xunit;

namespace Perfect.FileService.Infrastructure.Tests.IntegrationTests.Files
{
    public class BlobFileRepositoryTests : IClassFixture<AzuriteTestFixture>
    {
        private readonly IFixture _fixture;

        public BlobFileRepositoryTests(AzuriteTestFixture fixture)
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            _fixture.Inject(fixture.BlobClient);
            _fixture.Inject(fixture.BlobContainer);
        }

        [Fact]
        public async Task GetFileAsync_IfFileExists_ReturnModel()
        {
            // Arrange
            var fakeFileName = "test.txt";
            var fakeContent = "This is a test";
            var fakeData = Encoding.UTF8.GetBytes(fakeContent);

            var container = _fixture.Freeze<BlobContainerClient>();
            await CreateFileAsync(fakeFileName, fakeData);

            // Act
            var sut = _fixture.Create<BlobFileRepository>();
            var result = await sut.GetFileAsync(fakeFileName, CancellationToken.None);

            // Assert
            result.FileName.ShouldBe(fakeFileName);
            result.Length.ShouldBe(fakeData.Length);
            result.Content.ShouldBe(fakeData);

            // Clean up
            await container.DeleteBlobIfExistsAsync(fakeFileName);
        }

        [Fact]
        public async Task SaveFileAsync_CommitsToBlob()
        {
            // Arrange
            var fakeFileName = "test.txt";
            var fakeContent = "This is a test";
            var fakeData = Encoding.UTF8.GetBytes(fakeContent);
            var fakeFile = new FileEntity(fakeFileName, fakeData.Length, fakeData);

            var container = _fixture.Freeze<BlobContainerClient>();
            await CreateFileAsync(fakeFileName, fakeData);

            // Act
            var sut = _fixture.Create<BlobFileRepository>();
            await sut.SaveFileAsync(fakeFile, CancellationToken.None);

            // Assert
            var expected = await GetFileAsync(fakeFileName);

            fakeFile.Length.ShouldBe(expected.Length);
            fakeFile.Content.ShouldBe(expected);

            // Clean up
            await container.DeleteBlobIfExistsAsync(fakeFileName);
        }

        private async Task CreateFileAsync(string fileName, byte[] content, CancellationToken cancellationToken = default)
        {
            var container = _fixture.Freeze<BlobContainerClient>();
            await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            using (var memoryStream = new MemoryStream(content))
                await container.UploadBlobAsync(fileName, memoryStream, cancellationToken);
        }

        private async Task<byte[]> GetFileAsync(string fileName, CancellationToken cancellationToken = default)
        {
            var container = _fixture.Freeze<BlobContainerClient>();
            await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            var blob = container.GetBlobClient(fileName);

            using (var memoryStream = new MemoryStream())
            {
                await blob.DownloadToAsync(memoryStream, cancellationToken);
                return memoryStream.ToArray();
            }
        }
    }
}
