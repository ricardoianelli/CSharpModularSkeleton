using System.Data;
using Dapper;
using Database.Domain;
using Npgsql;

namespace Database.Application;

public class NpgsqlDatabase : IDatabase
{
    private readonly string _connectionString;

    public NpgsqlDatabase(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task Execute(string sqlCommand, object? commandParams, CancellationToken cancellationToken = default)
    {
        var connection = await CreateConnectionAsync(cancellationToken);
        await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sqlCommand, commandParams, cancellationToken: cancellationToken));
    }
    
    public async Task<T?> Query<T>(string sqlCommand, object? queryParams, CancellationToken cancellationToken = default)
    {
        var connection = await CreateConnectionAsync(cancellationToken);
        var result =
            await connection.QueryAsync<T>(
                new CommandDefinition(sqlCommand, queryParams, cancellationToken: cancellationToken));

        if (result is T convertedResult)
        {
            return convertedResult;
        }
        
        return default;
    }

    private async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}