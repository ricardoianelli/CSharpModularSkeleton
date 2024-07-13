using Newtonsoft.Json;

namespace Messaging.Api;

public class Message
{
    public readonly string Topic;
    public readonly object? Payload;
    
    public Message(string topic, object? payload)
    {
        Topic = topic;
        Payload = payload;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public bool TryUnpack<T>(out T? payload)
    {
        if (Payload?.GetType() == typeof(T))
        {
            payload = (T) Payload;
            return true;
        }

        payload = default;
        return false;
    }
}