using Microsoft.AspNetCore.Builder;
using Shared.Api;

namespace Logging.Api;

public abstract class ModuleInitializer : ICoreModule
{
    public static async Task Initialize()
    {
        await Logger.Initialize();
    }

    public static async Task InjectDependencies(IServiceProvider services)
    {
        await Logger.InjectDependencies(services);
    }

    public static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        await Logger.RegisterEndpoints(endpointsRegistry);
    }
}