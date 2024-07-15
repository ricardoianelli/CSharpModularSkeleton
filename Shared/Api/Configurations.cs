using Microsoft.Extensions.Configuration;

namespace Shared.Api;

public static class Configurations
{
    private static ConfigurationManager? _configuration;

    public static string? Get(string key)
    {
        return _configuration?[key];
    }

    public static void SetConfiguration(ConfigurationManager? configuration)
    {
        _configuration = configuration;
    }
}