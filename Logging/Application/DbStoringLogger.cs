using Logging.Application.Sql;
using Logging.Domain;
using Newtonsoft.Json;

namespace Logging.Application;

public class DbStoringLogger : ILogger
{
    public async Task Initialize()
    {
        await Database.Api.Database.Execute(Queries.CreateLogsTable);
    }

    public void Log(string msg)
    {
        LogAsync(msg).Wait();
    }

    public void Log(object obj)
    {
        LogAsync(obj).Wait();
    }

    public void Log(Exception ex)
    {
        LogAsync(ex).Wait();
    }

    public async Task LogAsync(string msg)
    {
        await Database.Api.Database.Execute(Queries.AddLog, new { message = msg });
    }

    public async Task LogAsync(object obj)
    {
        await LogAsync(JsonConvert.SerializeObject(obj));
    }

    public async Task LogAsync(Exception ex)
    {
        await LogAsync(JsonConvert.SerializeObject(ex));
    }
}