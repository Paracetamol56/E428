using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Variable : MonoBehaviour
{
    public static int Difficulty_Level = 1;
    public static float Player_Health_Normalised = 1;
    
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
}
