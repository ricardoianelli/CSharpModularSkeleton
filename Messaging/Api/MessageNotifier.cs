using Messaging.Application;
using Messaging.Domain;
using Microsoft.AspNetCore.Builder;

namespace Messaging.Api;

public abstract class MessageNotifier
{
    private static IMessageBroker _broker;
    
    internal static void SetBroker(IMessageBroker broker)
    {
        _broker = broker;
    }
    
    public static async Task Publish(string topic, Message message)
    {
        await _broker.Publish(topic, message);
        await _broker.Publish(Topics.Global, message);
    }
    
    public static async Task Publish(string topic, object payload)
    {
        var message = new Message(topic, payload);
        await Publish(topic, message);
    }
    
    public static async Task Publish(Message message)
    {
        await Publish(message.Topic, message);
    }
    
    public static async Task Subscribe(string topic, Action<Message> handler)
    {
        await _broker.Subscribe(topic, handler);
    }

    internal static async Task Initialize()
    {
        _broker = new CSharpMessageBroker();
    }

    internal static async Task InjectDependencies(IServiceProvider services)
    {
    }

    internal static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
    }
}