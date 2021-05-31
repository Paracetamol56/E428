using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_States : MonoBehaviour
{
    // Parametters
    public HUD_Manager HUD;
    [SerializeField]
    private int Cinematic_Id = 0;
    // variables
    private Mob_Basic_Attack Store_Janitor_HitBox ;
    private Store_Janitor_Mouvements Store_Janitor_Mo;
    private Store_Janitor_Attack Store_Janitor_Att;
    private Store_Janitor_Health Store_Janitor_He;
    private Boss_States State = Boss_States.Cinematic;
    private bool Has_Died = false;
    // Start is called before the first frame update
    void Start()
    {
        Store_Janitor_He = GetComponent<Store_Janitor_Health>();
        Store_Janitor_HitBox = GetComponentInChildren<Mob_Basic_Attack>();
        Store_Janitor_Mo = GetComponent<Store_Janitor_Mouvements>();
        Store_Janitor_Att = GetComponent<Store_Janitor_Attack>();
        // Call event
        Update_State(State);
        // Subscribe to event
        Event_System.current.onCinematicEnd += Begin_Combat;
        
    }

    // Update state and launch apropriate functions
    public void Update_State(Boss_States state)
    {
        State = state;
        if (!Has_Died)
        {
            switch (State)
            {
                case Boss_States.Cinematic:
                    break;
                case Boss_States.Attack:
                    
                    HUD.Start_Battle();
                    break;
                case Boss_States.Wait:
                    break;
                case Boss_States.Dead:
                    HUD.End_Battle();
                    Store_Janitor_HitBox.Is_Enabled = false;
                    // Update other scripts
                    Store_Janitor_Mo.Update_State(State);
                    Store_Janitor_Att.Update_State(State);
                    // Set variable
                    Has_Died = true;

                    // Fade out boss music volume 
                    Audio_Mixer_Control.current.Fade_Boss(-80, 0.5f);
                    break;
                default:
                    break;
            }
            // Update movement
            Store_Janitor_Mo.Update_State(State);
            Store_Janitor_Att.Update_State(State);
            Store_Janitor_He.Update_State(State);
            Debug.Log("Updated state to " + State);
        }
    }
    private void Begin_Combat(int id)
    {
        // If end cinematic has the right id 
        if (id == Cinematic_Id)
        {
            // Begin cinematic
            Update_State(Boss_States.Attack);
            // Launch boss music
            GetComponent<AudioSource>().Play();
            // Fade in boss music volume 
            Audio_Mixer_Control.current.Fade_Boss(-10, 1f);
            // Fade out music volume 
            Audio_Mixer_Control.current.Fade_Music(-80, 0.4f);
        }
    }
    
    
}
