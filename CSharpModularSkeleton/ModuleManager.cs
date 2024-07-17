using System.Reflection;
using Shared.Api;

namespace CSharpModularSkeleton;

internal static class ModuleManager
{
    private static List<IModule> _modules = [];
    
    public static async Task Start(WebApplication app)
    {
        await SetupCoreModules(app);
        await SetupExtraModules(app);
    }

    private static async Task SetupCoreModules(WebApplication app)
    {
        await SetupDatabase(app);
        await SetupLogger(app);
        await SetupMessageBroker(app);
    }

    private static async Task SetupDatabase(WebApplication app)
    {
        await Database.Api.ModuleInitializer.Initialize();
        await Database.Api.ModuleInitializer.InjectDependencies(app.Services);
        await Database.Api.ModuleInitializer.RegisterEndpoints(app);
    }
    
    private static async Task SetupLogger(WebApplication app)
    {
        await Logging.Api.ModuleInitializer.Initialize();
        await Logging.Api.ModuleInitializer.InjectDependencies(app.Services);
        await Logging.Api.ModuleInitializer.RegisterEndpoints(app);
    }
    
    private static async Task SetupMessageBroker(WebApplication app)
    {
        await Messaging.Api.ModuleInitializer.Initialize();
        await Messaging.Api.ModuleInitializer.InjectDependencies(app.Services);
        await Messaging.Api.ModuleInitializer.RegisterEndpoints(app);
    }
    
    private static async Task SetupExtraModules(WebApplication app)
    {
        await InitializeModules();
        await InjectDependencies(app.Services);
        await RegisterEndpoints(app);
    }
    
    
    private static async Task InitializeModules()
    {
        var tasks = new List<Task>();
        //AutoDiscoverModules();
        ManuallyRegisterModules();
        
        foreach (var module in _modules)
        {
            tasks.Add(module.Initialize());
        }
        
        await Task.WhenAll(tasks);
    }

    private static void ManuallyRegisterModules()
    {
        _modules.Add(new Safety.Api.ModuleInitializer());
        _modules.Add(new DangerousMotor.Api.ModuleInitializer());
        _modules.Add(new Counter.Api.ModuleInitializer());
        _modules.Add(new UI.Api.ModuleInitializer());
    }

    private static async Task InjectDependencies(IServiceProvider services)
    {
        var tasks = new List<Task>();
        
        foreach (var module in _modules)
        {
            tasks.Add(module.InjectDependencies(services));
        }

        await Task.WhenAll(tasks);
    }

    private static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        var tasks = new List<Task>();
        
        foreach (var module in _modules)
        {
            tasks.Add(module.RegisterEndpoints(endpointsRegistry));
        }
        
        await Task.WhenAll(tasks);
    }

    //I would still have to manually create references, I'll think about this later.
    private static void AutoDiscoverModules()
    {
        List<IModule> modules = [];
        try
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var assemblyName in Directory.GetFiles(currentPath, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(assemblyName);

                var moduleTypes = assembly.GetTypes()
                    .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var type in moduleTypes)
                {
                    var module = (IModule)Activator.CreateInstance(type);
                    if (module is null) throw new Exception($"Error discovering module {type.Assembly.FullName}.");

                    modules.Add(module);
                }
            }
        }
        catch (Exception ex)
        {
            Logging.Api.Logger.Log($"Exception during module discovery: {ex.Message}");
        }

        _modules = modules;
    }
}