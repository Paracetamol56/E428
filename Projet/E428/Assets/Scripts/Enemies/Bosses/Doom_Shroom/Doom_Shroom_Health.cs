using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Health : MonoBehaviour, IAttackable
{
    // Parametters
    [SerializeField]
    private int Health_Easy = 10;
    [SerializeField]
    private int Health_Normal = 12;
    [SerializeField]
    private int Health_Hard = 16;


    // Variables
    private HUD_Manager HUD;
    private int Max_Health;
    private int Current_Health;
    private Doom_Shroom_Animation Doom_Shroom_An;
    private Doom_Shroom_State Doom_Shroom_St;

    

    // Start is called before the first frame update
    void Start()
    {
        Doom_Shroom_St = GetComponent<Doom_Shroom_State>();
        Doom_Shroom_An = GetComponent<Doom_Shroom_Animation>();
        HUD = Doom_Shroom_St.HUD;
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Max_Health = Health_Hard;
                break;
            case 1:
                Max_Health = Health_Normal;
                break;
            default:
                Max_Health = Health_Easy;
                break;
        }
        Current_Health = Max_Health;
    }
    public void Be_Attacked()
    {
        // If RGB_Slime is in combat
        if (Doom_Shroom_St.Get_States() != Boss_States.Cinematic && Doom_Shroom_St.Get_States() != Boss_States.Dead)
        {
            // Make boss angrier
            Current_Health -= 1;
            Debug.Log("Doom Shroom Health : " + Current_Health);
            // If the boss has heath, he become stunt else he die
            if (Current_Health > 2)
            {
                // Debug
                Debug.Log("Doom shroom : Ouch");
                // Update To Attac so if Doom Shroom is waiting he will jump
                Doom_Shroom_St.Update_State(Boss_States.Attack);
                Doom_Shroom_An.Launch_Stunt_Animation();
            }
            else
            {
                Doom_Shroom_St.Update_State(Boss_States.Dead);
                Doom_Shroom_An.Launch_Death_Animation();
            }

            HUD.Update_Boss_Health_Bar(Current_Health, Max_Health);
        }
    }
}
