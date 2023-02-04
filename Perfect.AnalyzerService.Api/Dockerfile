FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Perfect.AnalyzerService.Api/Perfect.AnalyzerService.Api.csproj", "Perfect.AnalyzerService.Api/"]
RUN dotnet restore "Perfect.AnalyzerService.Api/Perfect.AnalyzerService.Api.csproj"
COPY . .
WORKDIR "/src/Perfect.AnalyzerService.Api"
RUN dotnet build "Perfect.AnalyzerService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Perfect.AnalyzerService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Perfect.AnalyzerService.Api.dll"]
