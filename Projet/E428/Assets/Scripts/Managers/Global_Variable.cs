using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Took the place of the original game manager
public class Global_Variable : MonoBehaviour
{
    public static int Difficulty_Level = 1;
    public static float Player_Health_Normalised = 1;
    public static int Last_Level_Build_Index = 0;
    public static Game_State Current_State = Game_State.Player_Control;
    public static List<string> Global_Dialog;


    // Update Difficulty
    public static void Update_Difficulty_Level(int diff)
    {

        // Prevent illegal value
        switch (diff)
        {
            case 2:
                Difficulty_Level = 2;
                break;
            case 1:
                Difficulty_Level = 1;
                break;
            default:
                Difficulty_Level = 0;
                break;
        }

        print(Difficulty_Level);
    }
    public static void Update_Game_State(Game_State State)
    {
        Current_State = State;
    }
    public static void Update_Dialog(List<string> Dialog)
    {
        Global_Dialog = Dialog;
    }
}
public enum Boss_States
{
    Cinematic,
    Attack,
    Wait,
    Dead
}
    // Enums
    public enum Game_State
    {
        Player_Control,
        Cinematic,
        Pause,
        Victory
    }

