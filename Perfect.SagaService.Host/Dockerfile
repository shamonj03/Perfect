#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Perfect.SagaService.Host/Perfect.SagaService.Host.csproj", "Perfect.SagaService.Host/"]
COPY ["Perfect.Messages/Perfect.Messages.csproj", "Perfect.Messages/"]
RUN dotnet restore "Perfect.SagaService.Host/Perfect.SagaService.Host.csproj"
COPY . .
WORKDIR "/src/Perfect.SagaService.Host"
RUN dotnet build "Perfect.SagaService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Perfect.SagaService.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Perfect.SagaService.Host.dll"]