using MassTransit;
using Perfect.AnalyzerService.Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AnalyzeFileConsumer>();

    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();