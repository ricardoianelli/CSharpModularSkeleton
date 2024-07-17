using Messaging.Api;
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
            // 1% chance of changing E-Stop state every 100ms.
            if (GetInt32(0, 10) > 5)
            {
                _isPressed = !_isPressed;
                await MessageNotifier.Publish(Topics.EStop, _isPressed);
            }
            
            await Task.Delay(CheckDelayInMs);
        }
    }
}