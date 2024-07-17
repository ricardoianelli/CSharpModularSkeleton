using Logging.Api;
using Messaging.Api;

namespace Counter.Application;

internal static class PacketCounter
{
    public static void Initialize()
    {
        Logger.Log("PacketCounter module initialized.");
        MessageNotifier.Subscribe("ui/start_counting", OnCountingRequested);
    }

    private static void OnCountingRequested(Message obj)
    {
        var count = (int)obj.Payload;
        var counted = 0;
        
        for (var i = 0; i < 10; i++)
        {
            counted++;
            Thread.Sleep(1000);
        }

        MessageNotifier.Publish("counter/target_reached", counted);
    }
}