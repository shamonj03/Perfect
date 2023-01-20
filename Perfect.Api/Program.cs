using Perfect.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();
builder.Build()
    .RegisterApplication()
    .Run();