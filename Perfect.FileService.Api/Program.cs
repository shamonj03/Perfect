using Perfect.FileService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);
builder.Build()
    .RegisterApplication()
    .Run();