using Messaging.Api;
using Safety.Api;
using static System.Security.Cryptography.RandomNumberGenerator;
using Topics = Safety.Api.Topics;

namespace Safety.Application;

internal static class EStop
{
    private static bool _isPressed = false;
    private const int CheckDelayInMs = 1000;
    
    public static async Task Start()
    {
        while (true)
        {
            // 50% chance of changing E-Stop state every 1000ms.
            if (GetInt32(0, 10) > 5)
            {
                _isPressed = !_isPressed;
                await MessageNotifier.Publish(Topics.EStop, new EStopStateChangedEvent(_isPressed));
            }
            
            await Task.Delay(CheckDelayInMs);
        }
    }
}