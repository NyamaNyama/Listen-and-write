using System;

public class GameEvents 
{
    public static event Action OnObjectInteractionStart;
    public static event Action OnObjectInteractionEnd;

    public static void TriggerInteracionStart() => OnObjectInteractionStart?.Invoke();
    public static void TriggerInteracionEnd() => OnObjectInteractionEnd?.Invoke();
}
