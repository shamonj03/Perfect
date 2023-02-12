using FluentValidation;
using Perfect.AnalyzerService.Api.Configuration;
using Perfect.AnalyzerService.Api.Configuration.Models;
using Perfect.AnalyzerService.Application.BannedWords;
using Perfect.AnalyzerService.Application.BannedWords.Interfaces;
using Perfect.AnalyzerService.Application.Common;
using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.OddLetters;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.AnalyzerService.Infrastructure.Common;
using Perfect.AnalyzerService.Infrastructure.HttpClients;

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
            services.AddScoped<IOddLetterAnalyzer, OddLetterAnalyzer>();
            services.AddScoped<IOddLetterAnalyzerService, OddLetterAnalyzerService>();
            services.AddScoped<IBannedWordAnalyzer, BannedWordAnalyzer>();
            services.AddScoped<IBannedWordAnalyzerService, BannedWordAnalyzerService>();

            // Infrastructure
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddHttpClient<IFileServiceClient, FileServiceClient>(x =>
            {
                x.BaseAddress = new Uri("https://localhost:7214");
            });
        }
    }
}
