using System;
using UnityEngine;

public class Event_System : MonoBehaviour
{
    public static Event_System current;
    // Create a static instance
    private void Awake()
    {
        current = this;
    }
    // Event

    public event Action onPipeEntered;
    public void Pipe_Entered()
    {
        if (onPipeEntered != null)
        {
            onPipeEntered?.Invoke();
        }
    }
    public event Action onReloadLevel;
    public void Reload_Level()
    {
        if (onReloadLevel != null)
        {
            onReloadLevel?.Invoke();
        }
    }
    
}
