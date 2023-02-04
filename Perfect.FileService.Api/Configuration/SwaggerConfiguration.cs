using Microsoft.Extensions.Options;
using Perfect.FileService.Api.Configuration.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Perfect.FileService.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
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
        }
    }
}
