namespace Database.Application.Sql;

public static class Queries
{
    public const string CheckDatabaseExistence = @"SELECT 1 FROM pg_database WHERE datname = @DbName;";
    
    public const string CreateDatabase = @"CREATE DATABASE @DbName;";
}