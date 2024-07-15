using Microsoft.AspNetCore.Builder;

namespace Shared.Api;

public interface IModule
{
    Task Initialize();
    Task InjectDependencies(IServiceProvider services);
    Task RegisterEndpoints(WebApplication endpointsRegistry);
}