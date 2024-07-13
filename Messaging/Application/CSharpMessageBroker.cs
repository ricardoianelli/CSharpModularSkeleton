using Messaging.Api;
using Messaging.Domain;

namespace Messaging.Application;

internal class CSharpMessageBroker : IMessageBroker
{
    private readonly Dictionary<string, Action<Message>> _topics = new();

    public Task Connect(ConnectionParams connectionParams)
    {
        return Task.CompletedTask;
    }

    public Task Disconnect()
    {
        return Task.CompletedTask;
    }

    public Task Publish(string topic, Message message)
    {
        if (_topics.TryGetValue(topic, out var listeners))
        {
            listeners?.Invoke(message);
        }
        
        return Task.CompletedTask;
    }

    public Task Subscribe(string topic, Action<Message> handler)
    {
        if (!_topics.TryAdd(topic, handler))
        {
            _topics[topic] += handler;
        }

        return Task.CompletedTask;
    }

    public Task Unsubscribe(string topic, Action<Message> handler)
    {
        try
        {
            if (_topics.ContainsKey(topic))
            {
                _topics[topic] -= handler;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an exception trying to unsubscribe from a message: " + e);
        }
        
        return Task.CompletedTask;
    }
}