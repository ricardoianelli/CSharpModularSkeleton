using Shared.Api;

namespace CSharpModularSkeleton;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Configurations.SetConfiguration(builder.Configuration);

        builder.Services.AddAuthorization();
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        await ModuleManager.Start(app);
        await app.RunAsync();
    }
}