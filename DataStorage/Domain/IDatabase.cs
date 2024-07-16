namespace Database.Domain;

public interface IDatabase
{
    Task Execute(string sqlCommand, Dictionary<string, string>? commandParams, CancellationToken cancellationToken = default);
    Task<T?> Query<T>(string sqlCommand, Dictionary<string, string>? queryParams,
        CancellationToken cancellationToken = default);
}