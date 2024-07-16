using System.Data;
using Dapper;

namespace Database.Domain;

public abstract class SqlDatabase
{
    protected virtual async Task InternalExecute(IDbConnection connection, string sqlCommand, Dictionary<string, string>? commandParams, CancellationToken cancellationToken = default)
    {
        sqlCommand = EnrichSql(sqlCommand, commandParams);
        
        await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sqlCommand, cancellationToken: cancellationToken));
    } 
    
    protected virtual async Task<T?> InternalQuery<T>(IDbConnection connection, string sqlCommand, Dictionary<string, string>? queryParams, CancellationToken cancellationToken = default)
    {
        sqlCommand = EnrichSql(sqlCommand, queryParams);
        
        var result =
            await connection.QueryAsync<T>(
                new CommandDefinition(sqlCommand, cancellationToken: cancellationToken));

        if (result is T convertedResult)
        {
            return convertedResult;
        }
        
        return default;
    } 
    
    protected virtual string EnrichSql(string sql, Dictionary<string, string>? parameters)
    {
        if (parameters is null) return sql;
        
        foreach (var (key, value) in parameters)
        {
            sql = sql.Replace($"@{key}", value);
        }
        
        return sql;
    }
}