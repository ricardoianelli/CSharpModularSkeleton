using Microsoft.AspNetCore.Builder;
using Shared.Api;
using UI.Application;

namespace UI.Api;

public class ModuleInitializer : IModule
{
    public async Task Initialize()
    {
        WebUI.Initialize();
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