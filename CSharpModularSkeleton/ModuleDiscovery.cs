using System.Reflection;
using Logging.Api;
using Shared.Api;

namespace CSharpModularSkeleton;

internal static class ModuleDiscovery
{
    private static List<IModuleInitializer> _modules = [];

    public static void Start()
    {
        AutoDiscoverModules();
        foreach (var module in _modules)
        {
            module.Initialize();
        }
    }

    public static void InjectDependencies(IServiceProvider services)
    {
        foreach (var module in _modules)
        {
            module.InjectDependencies(services);
        }
    }

    public static void RegisterEndpoints(WebApplication endpointsRegistry)
    {
        foreach (var module in _modules)
        {
            module.RegisterEndpoints(endpointsRegistry);
        }
    }

    private static void AutoDiscoverModules()
    {
        List<IModuleInitializer> modules = [];
        try
        {
            foreach (var assemblyName in Directory.GetFiles(
                         Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll"))
            {
                var assembly = Assembly.LoadFrom(assemblyName);

                var moduleTypes = assembly.GetTypes()
                    .Where(t => typeof(IModuleInitializer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var type in moduleTypes)
                {
                    var module = (IModuleInitializer)Activator.CreateInstance(type);
                    if (module is null) throw new Exception($"Error discovering module {type.Assembly.FullName}.");

                    modules.Add(module);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Exception during module discovery: {ex.Message}");
        }

        _modules = modules;
    }
}