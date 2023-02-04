namespace Perfect.AnalyzerService.Api.Common.Interfaces
{
    public interface IModule
    {
        void RegisterEndpoints(IEndpointRouteBuilder endpoints);
    }
}
