using Counter.Application;
using Microsoft.AspNetCore.Builder;
using Shared.Api;

namespace Counter.Api;

public class ModuleInitializer : IModule
{
    public async Task Initialize()
    {
        PacketCounter.Initialize();
    }

    public Task InjectDependencies(IServiceProvider services)
    {
        return Task.CompletedTask;
    }

    public Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        return Task.CompletedTask;
    }
}