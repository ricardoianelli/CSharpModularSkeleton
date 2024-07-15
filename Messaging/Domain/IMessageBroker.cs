using Messaging.Api;

namespace Messaging.Domain;

public interface IMessageBroker
{
    Task Connect(ConnectionParams connectionParams);
    Task Disconnect();
    Task Publish(string topic, Message message);
    Task Subscribe(string topic, Action<Message> handler);
    Task Unsubscribe(string topic, Action<Message> handler);
}