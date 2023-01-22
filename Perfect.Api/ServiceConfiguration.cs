using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Perfect.Api.Configuration;
using Perfect.Application.Orders;
using Perfect.Application.Orders.Interfaces;
using Perfect.Application.Users.Interfaces;
using Perfect.Infrastructure.Factories;
using Perfect.Infrastructure.Persistence.Interfaces;
using Perfect.Infrastructure.Persistence.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Perfect.Api
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

            services.AddScoped<IUserFactory, UserFactory>();
            
            services.AddScoped<IOrderFactory, OrderFactory>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("MongoDb")));

            services.AddScoped(x =>
            {
                var client = x.GetRequiredService<IMongoClient>();
                var database = configuration.GetSection("Mongo").GetValue<string>("Database");

                if (string.IsNullOrEmpty(database))
                    throw new Exception("Database name cannot be empty");

                return client.GetDatabase(database);
            });
        }
    }
}
