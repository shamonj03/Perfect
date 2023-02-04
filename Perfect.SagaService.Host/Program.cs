using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Perfect.SagaService.Host.Configuration.Models;
using Perfect.SagaService.Host.Consumers;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        // Application Settings
        services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
        services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));

        services.AddMassTransit(x =>
        {
            x.AddConsumer<FileReceivedConsumer>();

            //x.UsingAzureServiceBus((context, cfg) =>
            //{
            //    var settings = context.GetRequiredService<IOptions<AzureServiceBusSettings>>();
            //    cfg.Host(settings.Value.ConnectionString);
            //
            //    cfg.SubscriptionEndpoint<FileReceivedEvent>("saga-file-recieved-event", e =>
            //    {
            //        e.Consumer<FileReceivedConsumer>();
            //    });
            //});

            x.UsingRabbitMq((context, cfg) =>
            {
                var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>();
                cfg.Host(settings.Value.ConnectionString);

                cfg.ReceiveEndpoint("saga-file-recieved-event", x =>
                {
                    x.Consumer<FileReceivedConsumer>();
                });
            });
        });
    })
    .Build()
    .RunAsync();