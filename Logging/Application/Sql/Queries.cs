namespace Logging.Application.Sql;

public static class Queries
{
    public const string CreateLogsTable = @"
        CREATE TABLE IF NOT EXISTS Logs (
            id INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
            message TEXT NOT NULL,
            creation_date TIMESTAMP DEFAULT current_timestamp
        );";
    
    public const string AddLog = @"
        INSERT INTO Logs (message)
        VALUES (@message);
    ";
}