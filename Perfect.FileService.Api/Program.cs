using Perfect.FileService.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);
builder.Build()
    .RegisterApplication()
    .Run();