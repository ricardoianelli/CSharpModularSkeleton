using Microsoft.AspNetCore.Builder;

namespace Shared.Api;

public interface IModuleInitializer
{
    void Initialize();
    void InjectDependencies(IServiceProvider services);
    void RegisterEndpoints(WebApplication endpointsRegistry);
}