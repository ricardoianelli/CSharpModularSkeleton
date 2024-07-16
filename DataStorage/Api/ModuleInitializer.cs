using Microsoft.AspNetCore.Builder;
using Shared.Api;

namespace Database.Api;

public abstract class ModuleInitializer : ICoreModule
{
    public static async Task Initialize()
    {
        await Database.Initialize();
    }

    public static async Task InjectDependencies(IServiceProvider services)
    {
        await Database.InjectDependencies(services);
    }

    public static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        await Database.RegisterEndpoints(endpointsRegistry);
    }
}