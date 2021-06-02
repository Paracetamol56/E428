using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity_Manager_State : MonoBehaviour
{
    // Parametters
    public HUD_Manager HUD;
    [SerializeField]
    private GameObject Attack_Box;
    // Variables
    private Boss_States State = Boss_States.Cinematic;
    private bool Has_Died = false;
    private Electricity_Manager_Movement Electricity_Manager_Mo;
    private Electricicy_Manager_Attack Electricicy_Manager_At;
    private Electricity_Manager_Animation Electricity_Manager_An;
    private BoxCollider2D Electricity_Manager_Hit_Box;
    // Start is called before the first frame update
    void Start()
    {
        // get component
        Electricity_Manager_Mo = GetComponent<Electricity_Manager_Movement>();
        Electricicy_Manager_At = GetComponent<Electricicy_Manager_Attack>();
        Electricity_Manager_An = GetComponent<Electricity_Manager_Animation>();
        Electricity_Manager_Hit_Box = GetComponent<BoxCollider2D>();
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
                    

                    // Update HUD
                    HUD.End_Battle();
                    // Fade out Boss Music slowly
                    Audio_Mixer_Control.current.Fade_Boss(-80, 0.4f);
                    // Disable hurt Box and hitbox
                    Electricity_Manager_Hit_Box.enabled = false;
                    Attack_Box.SetActive(false);
                    // Block any other state pdates
                    Has_Died = true;

                    
                    break;
                default:
                    break;
            }
            // Update States for all script in need
            Electricity_Manager_Mo.Update_State(state);
            Electricicy_Manager_At.Update_State(state);
            Electricity_Manager_An.Update_State(state);
            //Doom_Shroom_Att.Update_State(state);
            Debug.Log("Updated Boss states to " + Get_States());
        }

    }
    public Boss_States Get_States()
    {
        return State;
    }
}
