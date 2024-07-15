using Logging.Application;
using Logging.Domain;
using Microsoft.AspNetCore.Builder;

namespace Logging.Api;

public abstract class Logger
{
    private static ILogger _logger;
    
    internal static void SetLogger(ILogger logger)
    {
        _logger = logger;
    }
    
    public static void Log(string msg)
    {
        _logger.Log(msg);
    }

    internal static async Task Initialize()
    {
        _logger = new DbStoringLogger();
        await _logger.Initialize();
    }

    internal static async Task InjectDependencies(IServiceProvider services)
    {
    }

    internal static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
    }
}