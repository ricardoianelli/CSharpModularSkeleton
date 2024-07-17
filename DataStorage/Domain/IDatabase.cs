namespace Database.Domain;

public interface IDatabase
{
    Task Execute(string sqlCommand, Dictionary<string, object>? commandParams, CancellationToken cancellationToken = default);
    Task<T?> Query<T>(string sqlCommand, Dictionary<string, object>? queryParams,
        CancellationToken cancellationToken = default);
}