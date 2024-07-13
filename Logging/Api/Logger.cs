using Logging.Application;
using Logging.Domain;

namespace Logging.Api;

public static class Logger
{
    private static ILogger _logger;

    static Logger()
    {
        _logger = new ConsoleLogger();
    }

    internal static void SetLogger(ILogger logger)
    {
        _logger = logger;
    }
    
    public static void Log(string msg)
    {
        _logger.Log(msg);
    }
}