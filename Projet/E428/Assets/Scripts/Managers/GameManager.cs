using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance of the game manager
    public static GameManager Instance;
    // Game state enum
    public Game_State State;
    // Change in state event
    public static event Action<Game_State> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Update_Game_State(Game_State.Start_Menu);
    }



    public void Update_Game_State(Game_State newState)
    {
        State = newState;
        switch (State)
        {
            case Game_State.Start_Menu:
                break;
            case Game_State.Player_Control:
                break;
            case Game_State.Cinematic:
                break;
            case Game_State.Pause:
                break;
            case Game_State.Death:
                break;
            case Game_State.Respawn:
                break;
            case Game_State.Victory:
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(State);
    }
}

public enum Game_State
{
    Start_Menu,
    Player_Control,
    Cinematic,
    Pause,
    Death,
    Respawn,
    Victory
}