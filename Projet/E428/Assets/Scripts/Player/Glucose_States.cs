using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose_States : MonoBehaviour
{

    // Enumerator
    public enum Player_Emotion {Normal = 0, Curious = 1, Surprised = 2, Tired = 3, Angry =4};
    public enum Player_Control {Normal, Pipe, Cinematic, Stunt, Dead }
    
    // Private Variables
    Player_Emotion Glucose_Vibe;
    Player_Control Glucose_Control;
    //
    Glucose_Animation Glucose_An;
    Glucose_Mouvements Glucose_Mo;
    // Start is called before the first frame update
    void Start()
    {
        Glucose_Vibe = Player_Emotion.Normal;
        Glucose_Control = Player_Control.Normal;
        Glucose_An = GetComponent<Glucose_Animation>();
        Glucose_Mo = GetComponent<Glucose_Mouvements>();
        
    }
    public void Change_Glucose_Vibe(Player_Emotion Emotion)
    {
        Glucose_An.Change_Animation_Vibe(Emotion);
    }
    public void Change_Glucose_Controls(Player_Control Control)
    {
        Glucose_Control = Control;
        switch (Control)
        {
            case Player_Control.Normal:
                break;
            case Player_Control.Pipe:
                break;
            case Player_Control.Cinematic:
                break;
            case Player_Control.Stunt:
                break;
            case Player_Control.Dead:
                print("State Dead");
                Glucose_An.Launch_Death_Animation();
                // Call Reload event
                Event_System.current.Reload_Level();
                break;
            default:
                break;
        }
        Glucose_Mo.Toggle_Movement(Control);
        Glucose_An.Change_Animation_Control(Control);
        
    }
}
