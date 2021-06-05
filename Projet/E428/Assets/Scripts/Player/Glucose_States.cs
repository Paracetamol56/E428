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
    private Glucose_Animation Glucose_An;
    private Glucose_Controls Glucose_Mo;
    private Glucose_Attack Glucose_Att;
    // Start is called before the first frame update
    void Start()
    {
        Glucose_Vibe = Player_Emotion.Normal;
        Glucose_Control = Player_Control.Normal;
        Glucose_An = GetComponent<Glucose_Animation>();
        Glucose_Mo = GetComponent<Glucose_Controls>();
        Glucose_Att = GetComponent<Glucose_Attack>();
        Event_System.current.onCinematicBegin += Change_Control_To_Cinematic;
        Event_System.current.onCinematicEnd += Change_Control_To_Normal;
    }
    private void OnDestroy()
    {
        Event_System.current.onCinematicBegin -= Change_Control_To_Cinematic;
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
            case Player_Control.Dead:
                Glucose_An.Launch_Death_Animation();
                // Fade out Master sound before reoload
                Audio_Mixer_Control.current.Fade_Master(-80, 1.5f);

                // Call Reload event
                Event_System.current.Reload_Level();
                break;
            default:
                break;
        }

        
        // toggles other scripts controls
        Glucose_Mo.Toggle_Movement(Control);

        Glucose_An.Change_Animation_Control(Control);

        print("Glucose Control States updated to " + Glucose_Control);      
    }
    private void Change_Control_To_Cinematic(int id)// Id is not used but requierd for the function to function (function to function lol)
    {
        Glucose_Control = Player_Control.Cinematic;
    }
    private void Change_Control_To_Normal(int id)// Id is not used but requierd for the function to function
    {
        // Prevent potential case of changing glucose controle while he is dead
        if (Glucose_Control != Player_Control.Dead)
        {
            Glucose_Control = Player_Control.Normal;
        }
        
    }
    // Potential function (would need refactor to be usefull)
    private void Random_Vibe()
    {
        StartCoroutine(Vibe_Changer());
    }
    private IEnumerator Vibe_Changer()
    {
        // Wait for a ranged random amount of time
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        // Chose a random vibe 
        int vibe_id = Random.Range(0, 4);
        // Set player vibe to vibe id
        Glucose_Vibe = (Player_Emotion)vibe_id;
        // Update animation controller
        Change_Glucose_Vibe(Glucose_Vibe);
        // Relaunch Sequence
        Random_Vibe();
    }
}
