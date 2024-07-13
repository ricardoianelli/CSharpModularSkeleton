namespace CSharpModularSkeleton;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        ModuleDiscovery.Start();
        ModuleDiscovery.InjectDependencies(app.Services);
        ModuleDiscovery.RegisterEndpoints(app);

        app.Run();
    }
}