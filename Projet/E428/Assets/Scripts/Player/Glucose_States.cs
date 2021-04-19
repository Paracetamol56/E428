using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose_States : MonoBehaviour
{
    // Enumerator
    public enum Player_Emotion {Normal = 0, Curious = 1, Surprised = 2, Tired = 3, Angry =4};
    public enum Player_Control {Normal = 1, Pipe = 0, Cinematic = 0, Stunt = 0 }
    
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
        Glucose_Mo.Toggle_Movement(Control);
        Glucose_An.Change_Animation_Control(Control);
    }
}
