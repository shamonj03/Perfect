namespace Perfect.Api.Common.Interfaces
{
    public interface IModule
    {
        void RegisterEndpoints(IEndpointRouteBuilder endpoints);
    }
}
