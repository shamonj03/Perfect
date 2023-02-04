using Perfect.AnalyzerService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);
builder.Build()
    .RegisterApplication()
    .Run();