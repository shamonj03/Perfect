using MassTransit;
using Microsoft.Extensions.Hosting;
using Perfect.SagaService.Host.Consumers;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);

            });

            x.AddConsumer<FileReceivedConsumer>();
        });
    })
    .Build()
    .RunAsync();