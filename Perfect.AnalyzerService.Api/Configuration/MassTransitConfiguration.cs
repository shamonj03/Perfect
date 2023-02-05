using MassTransit;
using Microsoft.Extensions.Options;
using Perfect.AnalyzerService.Api.Configuration.Models;
using Perfect.AnalyzerService.Api.Consumers;

namespace Perfect.AnalyzerService.Api.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void RegisterMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<AnalyzeFileConsumer>();

                //x.UsingAzureServiceBus((context, cfg) =>
                //{
                //    var settings = context.GetRequiredService<IOptions<AzureServiceBusSettings>>();
                //    cfg.Host(settings.Value.ConnectionString);
                //});

                x.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>();
                    cfg.Host(settings.Value.ConnectionString);

                    cfg.ReceiveEndpoint("analyzer-service-analyze-command", y =>
                    {
                        //y.ConfigureConsumeTopology = false;
                        //y.Bind("analyze-command");
                        y.ConfigureConsumer<AnalyzeFileConsumer>(context);
                    });
                });
            });
        }
    }
}
