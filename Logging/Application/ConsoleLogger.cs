using Logging.Domain;
using Newtonsoft.Json;

namespace Logging.Application;

public class ConsoleLogger : ILogger
{
    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public void Log(string msg)
    {
        Console.WriteLine(msg);
    }

    public void Log(object obj)
    {
        Log(JsonConvert.SerializeObject(obj));
    }

    public void Log(Exception ex)
    {
        Log(JsonConvert.SerializeObject(ex));
    }

    public async Task LogAsync(string msg)
    {
        await Task.Run(() => Log(msg));
    }

    public async Task LogAsync(object obj)
    {
        await Task.Run(() => Log(obj));
    }

    public async Task LogAsync(Exception ex)
    {
        await Task.Run(() => Log(ex));
    }
}