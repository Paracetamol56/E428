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
    public event Action onPauseGame;
    public void Pause_Game()
    {
        Debug.Log("Event Pause");
        if (onPauseGame != null)
        {
            
            onPauseGame?.Invoke();
        }
    }
    public event Action<int> onCinematicBegin;
    public void Cinematic_Begin(int id)
    {
        Debug.Log("Event Cinematic began");
        if (onCinematicBegin != null)
        {
            Global_Variable.Current_State = Game_State.Cinematic;
            onCinematicBegin?.Invoke(id);
        }
    }
    public event Action<int> onCinematicEnd;
    public void Cinematic_End(int id)
    {
        Global_Variable.Current_State = Game_State.Player_Control;
        Debug.Log("Event Cinematic end began");
        if (onCinematicEnd != null)
        {
            
            onCinematicEnd?.Invoke(id);
        }
    }
    public event Action onDialogStarted;
    public void Dialog_Started()
    {
        Debug.Log("Event Dialog Started");
        if (onDialogStarted != null)
        {
            onDialogStarted?.Invoke();
        }
    }
}
