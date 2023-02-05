using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.Extensions.Options;
using Perfect.FileService.Api.Configuration.Models;
using Perfect.FileService.Api.Consumers;
using Perfect.Messages.Events;

namespace Perfect.FileService.Api.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void RegisterMassTransit(this IServiceCollection services)
        {
            services.AddSingleton<IMessageDataRepository>(x =>
            {
                var client = x.GetRequiredService<BlobServiceClient>();
                return client.CreateMessageDataRepository("message-data");
            });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<FileUploadConsumer>();

                //x.UsingAzureServiceBus((context, cfg) =>
                //{
                //    var settings = context.GetRequiredService<IOptions<AzureServiceBusSettings>>();
                //    cfg.Host(settings.Value.ConnectionString);
                //});

                x.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>();
                    var messageRepository = context.GetRequiredService<IMessageDataRepository>();

                    cfg.Host(settings.Value.ConnectionString);
                    cfg.UseMessageData(messageRepository);

                    cfg.ReceiveEndpoint("file-service-upload-command", y =>
                    {
                        y.ConfigureConsumer<FileUploadConsumer>(context);
                    });
                });
            });
        }
    }
}
