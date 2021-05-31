using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB_Slime_Health : MonoBehaviour,IAttackable
{
    [SerializeField]
    private int Health_Easy = 7;
    [SerializeField]
    private int Health_Normal = 8;
    [SerializeField]
    private int Health_Hard = 10;
    // Variables
    private HUD_Manager HUD;
    private int Max_Health;
    private int Current_Health;
    private RGB_Slime_Animation RGB_Slime_An;
    private RGB_Slime_States RGB_Slime_St;
    private RGB_Slime_Attack RGB_Slime_At;
    
    // Start is called before the first frame update
    void Start()
    {

        RGB_Slime_An = GetComponent<RGB_Slime_Animation>();
        RGB_Slime_St = GetComponent<RGB_Slime_States>();
        RGB_Slime_At = GetComponent<RGB_Slime_Attack>();
        HUD = RGB_Slime_St.HUD;
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
        if (RGB_Slime_St.Get_States() != Boss_States.Cinematic && RGB_Slime_St.Get_States() != Boss_States.Dead)
        {
            // Make boss angrier
            RGB_Slime_At.Increase_Difficulty();
            Current_Health -= 1;
            // If the boss has heath, he become stunt else he die
            if (Current_Health > 2)
            {

                RGB_Slime_An.Launch_Stunt_Animation();
            }
            else
            {
                RGB_Slime_St.Update_State(Boss_States.Dead);
                RGB_Slime_An.Launch_Death_Animation();
            }

            HUD.Update_Boss_Health_Bar(Current_Health, Max_Health);
        }
       
    }
    
}
