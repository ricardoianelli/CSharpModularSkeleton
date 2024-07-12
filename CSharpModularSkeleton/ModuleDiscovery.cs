using System.Reflection;
using Shared;

namespace CSharpModularSkeleton;

public static class ModuleDiscovery
{
    private static List<IModuleInitializer> _modules = [];

    public static Task Start()
    {
        LoadModules();
        
        foreach (var module in _modules)
        {
            module.Initialize();
        }

        return Task.CompletedTask;
    }

    public static Task InjectDependencies(IServiceCollection services)
    {
        foreach (var module in _modules)
        {
            module.InjectDependencies(services);
        }

        return Task.CompletedTask;
    }

    public static Task RegisterEndpoints(RouteGroupBuilder builder)
    {
        foreach (var module in _modules)
        {
            module.RegisterEndpoints(builder);
        }

        return Task.CompletedTask;
    }
    
    private static void LoadModules()
    {
        List<IModuleInitializer> modules = [];
        
        var currentAssembly = Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();

        foreach (var assemblyName in referencedAssemblies)
        {
            try
            {
                var assembly = Assembly.Load(assemblyName);

                var moduleTypes = assembly.GetTypes()
                    .Where(t => typeof(IModuleInitializer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var type in moduleTypes)
                {
                    var module = (IModuleInitializer) Activator.CreateInstance(type);
                    if (module is null) throw new Exception();
                    modules.Add(module);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load module {assemblyName.Name}: {ex.Message}");
            }
        }

        _modules = modules;
    }
}