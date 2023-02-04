using FluentValidation;
using Perfect.AnalyzerService.Api.Configuration;
using Perfect.AnalyzerService.Api.Configuration.Models;

namespace Perfect.AnalyzerService.Api
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Settings
            services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
            services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));

            // Other
            services.RegisterSwagger();
            services.RegisterMassTransit();

            // Application
            services.AddValidatorsFromAssemblyContaining<Program>();

            // Infrastructure
        }
    }
}
