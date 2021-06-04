using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB_Slime_States : MonoBehaviour
{
    
    
    // Parametters
    public HUD_Manager HUD;
    
    // variables
    private Mob_Basic_Attack RGB_Slime_HitBox;
    private RGB_Slime_Movement RGB_Slime_Mo;
    private RGB_Slime_Attack RGB_Slime_Att;
    private BoxCollider2D RGB_Slime_Collision;
    private Rigidbody2D RGB_Slime_RB;
    private Boss_Load_Next_Level Boss_Load_Next_Le;
    private Boss_States State = Boss_States.Cinematic;
    private bool Has_Died = false;

    // Start is called before the first frame update
    void Start()
    {

        RGB_Slime_HitBox = GetComponentInChildren<Mob_Basic_Attack>();
        RGB_Slime_Mo = GetComponent<RGB_Slime_Movement>();
        RGB_Slime_Att = GetComponent<RGB_Slime_Attack>();
        RGB_Slime_Collision = GetComponent<BoxCollider2D>();
        RGB_Slime_RB = GetComponent<Rigidbody2D>();
        Boss_Load_Next_Le = GetComponent<Boss_Load_Next_Level>();
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
                    RGB_Slime_RB.gravityScale = 0;
                    RGB_Slime_Collision.isTrigger = true;
                    // Update HUD
                    HUD.End_Battle();
                    // Disable hurt Box and 
                    RGB_Slime_HitBox.Is_Enabled = false;
                    RGB_Slime_Mo.Freeze();
                    // Block any other state pdates
                    Has_Died = true;
                    // Fade out Boss music
                    Audio_Mixer_Control.current.Fade_Boss(-80, 0.6f);
                    // Load next level
                    Boss_Load_Next_Le.Load_Next_Level();
                    break;
                default:
                    break;
            }
            // Update States for all script in need
            RGB_Slime_Mo.Update_State(state);
            RGB_Slime_Att.Update_State(state);
            Debug.Log("Updated Boss states to " + Get_States());
        }
        
    }
    public Boss_States Get_States()
    {
        return State;
    }
    
}
