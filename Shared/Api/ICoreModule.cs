using Microsoft.AspNetCore.Builder;

namespace Shared.Api;

public interface ICoreModule
{
    static abstract Task Initialize();
    static abstract Task InjectDependencies(IServiceProvider services);
    static abstract Task RegisterEndpoints(WebApplication endpointsRegistry);
}