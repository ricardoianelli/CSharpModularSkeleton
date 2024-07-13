namespace Messaging.Api;

public class ConnectionParams
{
    public string Host;
    public string UserName;
    public string Password;
    public int Port;

    public ConnectionParams(string host, int port, string userName, string password)
    {
        Host = host;
        UserName = userName;
        Password = password;
        Port = port;
    }
}