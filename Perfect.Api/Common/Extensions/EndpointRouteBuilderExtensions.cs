using Perfect.Api.Common.Interfaces;

namespace Perfect.Api.Common.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterEndpointModules(this IEndpointRouteBuilder builder)
        {
            var moduleType = typeof(IModule);

            var modules = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && moduleType.IsAssignableFrom(p));

            foreach (var module in modules)
            {
                var instance = Activator.CreateInstance(module) as IModule;

                instance?.RegisterEndpoints(builder);
            }
        }
    }
}
