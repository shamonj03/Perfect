using Azure.Storage.Blobs;
using FluentValidation;
using Microsoft.Extensions.Options;
using Perfect.FileService.Api.Configuration.Models;
using Perfect.FileService.Application.Common;
using Perfect.FileService.Application.Files;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Infrastructure.Common;
using Perfect.FileService.Infrastructure.Files;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Perfect.FileService.Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Settings
            services.Configure<BlobStorageSettings>(configuration.GetSection(BlobStorageSettings.Section));
            services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
            services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));

            //services.AddProblemDetails();

            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers
                // "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

            // Application
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            // Infrastructure
            services.RegisterMassTransit();
            services.AddScoped<IFileRepository, BlobFileRepository>();
            services.AddScoped<IMessageSender, MessageSender>();

            services.AddSingleton(x =>
            {
                var settings = x.GetRequiredService<IOptions<BlobStorageSettings>>();
                return new BlobServiceClient(settings.Value.ConnectionString);
            });
        }
    }
}
