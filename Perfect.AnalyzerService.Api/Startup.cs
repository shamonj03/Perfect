using FluentValidation;
using Microsoft.Extensions.Options;
using Perfect.AnalyzerService.Api.Common.Extensions;
using Perfect.AnalyzerService.Api.Common.Middleware;
using Perfect.AnalyzerService.Api.Configuration;
using Perfect.AnalyzerService.Application.BannedWords;
using Perfect.AnalyzerService.Application.BannedWords.Interfaces;
using Perfect.AnalyzerService.Application.Common;
using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.OddLetters;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.AnalyzerService.Application.Settings;
using Perfect.AnalyzerService.Infrastructure.Common;
using Perfect.AnalyzerService.Infrastructure.FileService;

namespace Perfect.AnalyzerService.Api
{
    public static class Startup
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Settings
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

            services.Configure<FileServiceSettings>(configuration.GetSection(FileServiceSettings.Section));
            services.PostConfigure<FileServiceSettings>(x =>
            {
                x.BaseUrl = configuration.GetConnectionString(FileServiceSettings.Section) ?? throw new ArgumentNullException();
            });

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
            services.AddHttpClient<IFileServiceClient, FileServiceClient>((provider, client) =>
            {
                var settings = provider.GetRequiredService<IOptions<FileServiceSettings>>();
                client.BaseAddress = new Uri(settings.Value.BaseUrl);
            });
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
