using MassTransit;

namespace Perfect.FileService.Api.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void RegisterMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) => 
                {
                    cfg.Host("localhost", "/", h => 
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
