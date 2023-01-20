using Perfect.Api.Common.Extensions;
using Perfect.Api.Middleware;

namespace Perfect.Api
{
    public static class WebApplicationConfiguration
    {
        public static WebApplication RegisterApplication(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<InternalErrorMiddleware>();

            app.NewVersionedApi()
                .MapGroup("/api/v{version:apiVersion}")
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
