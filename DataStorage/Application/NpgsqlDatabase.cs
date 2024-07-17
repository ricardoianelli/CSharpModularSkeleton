using System.Data;
using Dapper;
using Database.Domain;
using Npgsql;

namespace Database.Application;

public class NpgsqlDatabase : SqlDatabase, IDatabase
{
    private readonly string _connectionString;
    private string _tableCreationConnectionString;

    public NpgsqlDatabase(string connectionString)
    {
        _connectionString = connectionString;
        var dbName = GetDbNameFromConnectionString();
        ValidateDatabaseName(dbName);
        CreateDbIfInexistent(dbName);
    }

    private string GetDbNameFromConnectionString()
    {
        return _connectionString.Split("Database=")[1].Split(";")[0];
    }

    private void ValidateDatabaseName(string databaseName)
    {
        if (databaseName.Any(c => char.IsUpper(c)))
        {
            throw new ArgumentException("Database name must be lowercase in Postgres.");
        }
    }

    //TODO: Improve
    private void CreateDbIfInexistent(string dbName)
    {
        _tableCreationConnectionString = _connectionString.Replace(dbName, "postgres");
        var connection = new NpgsqlConnection(_tableCreationConnectionString);
        connection.Open();
        var result = connection.ExecuteScalar(Sql.Queries.CheckDatabaseExistence, new { DbName = dbName });

        //If it's not null, DB exists, so all good.
        if (result is not null) return;
        
        var queryParams = new Dictionary<string, object> { { "DbName", dbName } };
        InternalExecute(connection, Sql.Queries.CreateDatabase, queryParams).Wait();
    }

    public async Task Execute(string sqlCommand, Dictionary<string, object>? commandParams, CancellationToken cancellationToken = default)
    {
        var connection = await CreateConnectionAsync(cancellationToken);
        await InternalExecute(connection, sqlCommand, commandParams, cancellationToken);
    }
    
    public async Task<T?> Query<T>(string sqlCommand, Dictionary<string, object>? queryParams, CancellationToken cancellationToken = default)
    {
        var connection = await CreateConnectionAsync(cancellationToken);
        var result = await InternalQuery<T>(connection, sqlCommand, queryParams, cancellationToken);
        return result;
    }

    private async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}