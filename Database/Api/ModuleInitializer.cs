using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Database.Api;

public class ModuleInitializer : IModuleInitializer
{
    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public Task InjectDependencies(IServiceCollection services)
    {
        return Task.CompletedTask;
    }

    public Task RegisterEndpoints(RouteGroupBuilder builder)
    {
        return Task.CompletedTask;
    }
}