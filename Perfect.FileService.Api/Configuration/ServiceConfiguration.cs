using FluentValidation;
using Microsoft.Extensions.Options;
using Perfect.FileService.Application.Files;
using Perfect.FileService.Application.Files.Interfaces;
using Perfect.FileService.Infrastructure.Files;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Perfect.FileService.Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddValidatorsFromAssemblyContaining<Program>();

            // Infrastructure
            services.AddScoped<IFileRepository, BlobFileRepository>();

            // Application
            services.AddScoped<IFileUploadService, FileUploadService>();
        }
    }
}
