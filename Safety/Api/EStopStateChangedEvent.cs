namespace Safety.Api;

public class EStopStateChangedEvent
{
    public bool IsPressed;

    public EStopStateChangedEvent(bool state)
    {
        IsPressed = state;
    }
}