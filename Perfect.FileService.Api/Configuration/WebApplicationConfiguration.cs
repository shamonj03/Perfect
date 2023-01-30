using Perfect.FileService.Api.Common.Extensions;
using Perfect.FileService.Api.Common.Middleware;

namespace Perfect.FileService.Api.Configuration
{
    public static class WebApplicationConfiguration
    {
        public static WebApplication RegisterApplication(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<InternalErrorMiddleware>();

            app.NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}")
                .WithOpenApi()
                .RegisterEndpointModules();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
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
