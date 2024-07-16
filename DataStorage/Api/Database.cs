using Database.Application;
using Database.Domain;
using Microsoft.AspNetCore.Builder;
using Shared.Api;

namespace Database.Api;

public abstract class Database
{
    private static IDatabase Db;

    internal static void SetDatabase(IDatabase database)
    {
        Db = database;
    }
    
    public static async Task Execute(string sqlCommand, Dictionary<string, string>? commandParams = null, CancellationToken cancellationToken = default)
    {
        await Db.Execute(sqlCommand, commandParams, cancellationToken: cancellationToken);
    }
    
    public static async Task<T?> Query<T>(string sqlCommand, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
    {
        return await Db.Query<T>(sqlCommand, queryParams, cancellationToken: cancellationToken);
    }
    
    internal static async Task Initialize()
    {
        var connectionString = Configurations.Get("Database:ConnectionString");
        if (connectionString is null)
        {
            throw new Exception("Couldn't find a connection string for the database.");
        }
        
        Db = new NpgsqlDatabase(connectionString);
    }

    internal static async Task InjectDependencies(IServiceProvider services)
    {
    }

    internal static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
    }
}