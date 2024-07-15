namespace Logging.Domain;

public interface ILogger
{
    Task Initialize();
    
    void Log(string msg);
    void Log(object obj);
    void Log(Exception ex);
    
    Task LogAsync(string msg);
    Task LogAsync(object obj);
    Task LogAsync(Exception ex);
}