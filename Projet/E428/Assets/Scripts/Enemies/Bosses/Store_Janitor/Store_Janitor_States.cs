using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_States : MonoBehaviour
{
    // Parametters
    public HUD_Manager HUD;

    // variables
    private Mob_Basic_Attack Store_Janitor_HitBox ;
    private Store_Janitor_Mouvements Store_Janitor_Mo;
    private Store_Janitor_Attack Store_Janitor_Att;

    private Boss_States State = Boss_States.Attack;
    
    // Start is called before the first frame update
    void Start()
    {

        Store_Janitor_HitBox = GetComponentInChildren<Mob_Basic_Attack>();
        Store_Janitor_Mo = GetComponent<Store_Janitor_Mouvements>();
        Store_Janitor_Att = GetComponent<Store_Janitor_Attack>();
        // Call event
        Update_Sate(State);
    }

    // Update state and launch apropriate functions
    public void Update_Sate(Boss_States state)
    {
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
       
                HUD.End_Battle();
                Store_Janitor_HitBox.Is_Enabled = false;
                Store_Janitor_Mo.Update_State(state);
                Store_Janitor_Att.Update_State(state);
                break;
            default:
                break;
        }
        // Update movement
        Store_Janitor_Mo.Update_State(state);
        Store_Janitor_Att.Update_State(state);
    }
    
}
