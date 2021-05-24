using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance of the game manager
    public static GameManager Instance;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Update_Game_State(Game_State.Player_Control);
    }



    // Update global game_State
    public static void Update_Game_State(Game_State State)
    {
        Global_Variable.Update_Game_State(State);
        switch (Global_Variable.Current_State)
        {
            case Game_State.Player_Control:
                break;
            case Game_State.Cinematic:
                break;
            case Game_State.Pause:
                break;
            case Game_State.Victory:
                break;
            default:
                break;
        }
    }
}

