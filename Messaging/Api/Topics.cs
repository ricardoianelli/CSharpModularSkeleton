namespace Messaging.Api;

public static class Topics
{
    private static Dictionary<string, string> _topics = new()
    {
        ["start_packet_counting_request"] = "messaging/ui/start_counting",
        ["counter/target_reached"] = "messaging/counter/target_reached",
        ["global"] = "messaging/global"
    };
    
    public const string Global = "messaging/global";
    
    
}