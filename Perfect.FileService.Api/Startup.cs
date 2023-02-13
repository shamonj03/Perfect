using Azure.Storage.Blobs;
using FluentValidation;
using Microsoft.Extensions.Options;
using Perfect.FileService.Api.Common.Extensions;
using Perfect.FileService.Api.Common.Middleware;
using Perfect.FileService.Api.Configuration;
using Perfect.FileService.Application.Common;
using Perfect.FileService.Application.Files;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Domain.Services;
using Perfect.FileService.Infrastructure.Common;
using Perfect.FileService.Infrastructure.Files;
using Perfect.FileService.Infrastructure.Settings;

namespace Perfect.FileService.Api
{
    public static class Startup
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Settings
            services.Configure<BlobStorageSettings>(configuration.GetSection(BlobStorageSettings.Section));
            services.PostConfigure<BlobStorageSettings>(x =>
            {
                x.ConnectionString = configuration.GetConnectionString(BlobStorageSettings.Section) ?? throw new ArgumentNullException();
            });

            services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
            services.PostConfigure<AzureServiceBusSettings>(x =>
            {
                x.ConnectionString = configuration.GetConnectionString(AzureServiceBusSettings.Section);
            });

            services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));
            services.PostConfigure<RabbitMqSettings>(x =>
            {
                x.ConnectionString = configuration.GetConnectionString(RabbitMqSettings.Section);
            });

            // Application
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddScoped<IFileService, ConcreteFileService>();

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

        public static WebApplication RegisterApplication(this WebApplication app)
        {
            app.UseMiddleware<InternalErrorMiddleware>();

            app.NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}")
                .WithOpenApi()
                .RegisterEndpointModules();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var descriptions = app.DescribeApiVersions();

                    // build a swagger endpoint for each discovered API version
                    foreach (var description in descriptions)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });
            }
            return app;
        }
    }
}
