using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_State : MonoBehaviour
{
    // Parametters
    public HUD_Manager HUD;
    // Variable
    private Boss_States State = Boss_States.Cinematic;
    private bool Has_Died = false;
    private Rigidbody2D Doom_Shroom_RB;
    private BoxCollider2D Doom_Shroom_Collision;
    private Mob_Basic_Attack Doom_Shroom_HitBox;
    private Doom_Shroom_Movement Doom_Shroom_Mo;
    private Doom_Shroom_Attack Doom_Shroom_Att;
    private Boss_Load_Next_Level Boss_Load_Next_Le;
    // Start is called before the first frame update
    void Start()
    {
        
        // Get components
        Doom_Shroom_HitBox = GetComponentInChildren<Mob_Basic_Attack>();
        Doom_Shroom_Collision = GetComponent<BoxCollider2D>();
        Doom_Shroom_RB = GetComponent<Rigidbody2D>();
        Doom_Shroom_Att = GetComponent<Doom_Shroom_Attack>();
        Doom_Shroom_Mo = GetComponent<Doom_Shroom_Movement>();
        Boss_Load_Next_Le = GetComponent<Boss_Load_Next_Level>();
        Update_State(Boss_States.Cinematic);
    }

    public void Update_State(Boss_States state)
    {
        if (!Has_Died)
        {
            State = state;
            // Call functions expected functions when there is a new state 
            switch (state)
            {
                case Boss_States.Cinematic:
                    break;
                case Boss_States.Attack:
                    Doom_Shroom_Mo.Refresh_Ground_Check();
                    HUD.Start_Battle();
                    break;
                case Boss_States.Wait:
                    break;
                case Boss_States.Dead:
                    // Set Colision to trigger to make player able to walk "arround" the boss and stop gravity to prevent boss from falling ito the void    
                    Doom_Shroom_RB.gravityScale = 0;
                    Doom_Shroom_Collision.isTrigger = true;
                    // Update HUD
                    HUD.End_Battle();
                    // Fade out Boss Music slowly
                    Audio_Mixer_Control.current.Fade_Boss(-80,0.4f);
                    // Disable hurt Box and 
                    Doom_Shroom_HitBox.Is_Enabled = false;
                    // Block any other state pdates
                    Has_Died = true;
                    // Change level
                    Boss_Load_Next_Le.Load_Next_Level();
                    break;
                default:
                    break;
            }
            // Update States for all script in need
            Doom_Shroom_Mo.Update_State(state);
            Doom_Shroom_Att.Update_State(state);
            Debug.Log("Updated Boss states to " + Get_States());
        }

    }
    public Boss_States Get_States()
    {
        return State;
    }
}
