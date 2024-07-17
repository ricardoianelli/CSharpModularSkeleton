using DangerousMotor.Application;
using Logging.Api;
using Microsoft.AspNetCore.Builder;
using Shared.Api;

namespace DangerousMotor.Api;

public class ModuleInitializer : IModule
{
    public async Task Initialize()
    {
        Logger.Log("DangerousMotor module initialized!");
        await Motor.Start();
    }

    public async Task InjectDependencies(IServiceProvider services)
    {
        
    }

    public async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        
    }
}