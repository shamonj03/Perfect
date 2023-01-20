using Asp.Versioning;
using Microsoft.Extensions.Options;
using Perfect.Api.Configuration;
using Perfect.Application.Orders;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Users.Interfaces;
using Perfect.Infrastructure.Factories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Perfect.Api
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
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

            services.AddScoped<IUserFactory, UserFactory>();
            
            services.AddScoped<IOrderFactory, OrderFactory>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
