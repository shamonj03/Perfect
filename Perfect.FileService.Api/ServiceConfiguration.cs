using Azure.Storage.Blobs;
using FluentValidation;
using Microsoft.Extensions.Options;
using Perfect.FileService.Api.Configuration;
using Perfect.FileService.Api.Configuration.Models;
using Perfect.FileService.Application.Common;
using Perfect.FileService.Application.Files;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Infrastructure.Common;
using Perfect.FileService.Infrastructure.Files;

namespace Perfect.FileService.Api
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Settings
            services.Configure<BlobStorageSettings>(configuration.GetSection(BlobStorageSettings.Section));
            services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
            services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));

            // Application
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            // Infrastructure
            services.AddScoped<IFileRepository, BlobFileRepository>();
            services.AddScoped<IMessageSender, MessageSender>();

            services.AddSingleton(x =>
            {
                var settings = x.GetRequiredService<IOptions<BlobStorageSettings>>();
                return new BlobServiceClient(settings.Value.ConnectionString);
            });

            // Other
            services.RegisterSwagger();
            services.RegisterMassTransit();
        }
    }
}
