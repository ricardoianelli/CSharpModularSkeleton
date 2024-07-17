using Logging.Api;
using Messaging.Api;
using Safety.Api;

namespace DangerousMotor.Application;

internal static class Motor
{
    public static async Task Start()
    {
        await MessageNotifier.Subscribe(Safety.Api.Topics.EStop, OnEStopStateChange);
    }

    private static void OnEStopStateChange(Message obj)
    {
        if (obj.Payload is null) return;

        var message = (EStopStateChangedEvent)obj.Payload;
        
        // if (message.IsPressed == true)
        // {
        //     Logger.Log("Motor stopped.");
        // }
        // else
        // {
        //     Logger.Log("Motor started.");
        // }
    }
}