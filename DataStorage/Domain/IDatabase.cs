namespace Database.Domain;

public interface IDatabase
{
    Task Execute(string sqlCommand, object? commandParams, CancellationToken cancellationToken = default);
    Task<T?> Query<T>(string sqlCommand, object? queryParams, CancellationToken cancellationToken = default);
}