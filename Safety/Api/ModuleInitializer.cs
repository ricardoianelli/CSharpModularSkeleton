using Logging.Api;
using Microsoft.AspNetCore.Builder;
using Safety.Application;
using Shared.Api;

namespace Safety.Api;

public class ModuleInitializer : IModule
{
    public async Task Initialize()
    {
        EStop.Start();
        Logger.Log("Safety module initialized!");
    }

    public async Task InjectDependencies(IServiceProvider services)
    {
        
    }

    public async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        
    }
}