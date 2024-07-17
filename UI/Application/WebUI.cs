using Logging.Api;
using Messaging.Api;
using Microsoft.Extensions.Logging;

namespace UI.Application;

internal static class WebUI
{
    public static void Initialize()
    {
        Logger.Log("WebUI module initialized.");
        MessageNotifier.Subscribe("counter/target_reached", OnPacketTargetReached);
        MessageNotifier.Publish("ui/start_counting", 30);
    }

    private static void OnPacketTargetReached(Message obj)
    {
        var count = (int)obj.Payload;
        Logger.Log($"Finished counting {count} seeds in the packet!");
    }
}