using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity_Manager_State : MonoBehaviour
{
    // Parametters
    public HUD_Manager HUD;
    // Variables
    private Boss_States State = Boss_States.Attack;
    private bool Has_Died = false;
    private Electricity_Manager_Movement Electricity_Manager_Mo;
    private Electricicy_Manager_Attack Electricicy_Manager_At;
    // Start is called before the first frame update
    void Start()
    {
        // get component
        Electricity_Manager_Mo = GetComponent<Electricity_Manager_Movement>();
        Electricicy_Manager_At = GetComponent<Electricicy_Manager_Attack>();
        // Update all scripts states
        Update_State(State);
    }

    public void Update_State(Boss_States state)
    {
        if (!Has_Died)
        {
            State = state;
            switch (state)
            {
                case Boss_States.Cinematic:
                    break;
                case Boss_States.Attack:
                    HUD.Start_Battle();
                    break;
                case Boss_States.Wait:
                    break;
                case Boss_States.Dead:
                    // Set Colision to trigger to make player able to walk "arround" the boss and stop gravity to prevent boss from falling ito the void    

                    // Update HUD
                    HUD.End_Battle();
                    // Fade out Boss Music slowly
                    Audio_Mixer_Control.current.Fade_Boss(-80, 0.4f);
                    // Disable hurt Box and 
                    //Doom_Shroom_HitBox.Is_Enabled = false;

                    // Block any other state pdates
                    Has_Died = true;
                    break;
                default:
                    break;
            }
            // Update States for all script in need
            Electricity_Manager_Mo.Update_State(state);
            Electricicy_Manager_At.Update_State(state);
            //Doom_Shroom_Att.Update_State(state);
            Debug.Log("Updated Boss states to " + Get_States());
        }

    }
    public Boss_States Get_States()
    {
        return State;
    }
}
