using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Shared;

public interface IModuleInitializer
{
    Task Initialize();
    Task InjectDependencies(IServiceCollection services);
    Task RegisterEndpoints(RouteGroupBuilder builder);
}