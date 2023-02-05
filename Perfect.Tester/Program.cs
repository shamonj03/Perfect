using Azure.Storage.Blobs;
using MassTransit;
using MassTransit.AzureStorage.MessageData;
using Perfect.Messages.Commands;

namespace Perfect.Tester
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var blobClient = new BlobServiceClient("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://localhost:10000/devstoreaccount1;");
            var respository = new AzureStorageMessageDataRepository(blobClient, "message-data");
            
            // Sets where to send
            EndpointConvention.Map<FileUploadCommand>(new Uri("queue:file-service-upload-command"));

            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                x.Host("amqp://guest:guest@localhost:5672/");
                x.UseMessageData(respository);

                // Sets where to publish
                x.Message<FileUploadCommand>(y => y.SetEntityName("test"));
            });

            await respository.PreStart(bus);

            var content = "This is some content...";

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                await streamWriter.WriteAsync(content);
                await streamWriter.FlushAsync();
                memoryStream.Position = 0;

                //var endpoint = await bus.GetSendEndpoint(new Uri("queue:file-service-upload-command"));
                //var data = await respository.PutStream(memoryStream);
                await bus.Send<FileUploadCommand>(new 
                {
                    FileName = "test.text", 
                    Length = memoryStream.Length, 
                    Content = memoryStream.ToArray()
                });
            }
        }
    }
}
