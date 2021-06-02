using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity_Manager_Health : MonoBehaviour, IAttackable
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
    private Electricity_Manager_Animation Electricity_Manager_An;
    private Electricity_Manager_State Electricity_Manager_St;



    // Start is called before the first frame update
    void Start()
    {
        Electricity_Manager_An = GetComponent<Electricity_Manager_Animation>();
        Electricity_Manager_St = GetComponent<Electricity_Manager_State>();


        HUD = Electricity_Manager_St.HUD;
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
        if (Electricity_Manager_St.Get_States() != Boss_States.Cinematic && Electricity_Manager_St.Get_States() != Boss_States.Dead)
        {
            // Make boss angrier
            Current_Health -= 1;
            Debug.Log("Electricity Manager Health : " + Current_Health);
            // If the boss has heath, he become stunt else he die
            if (Current_Health > 2)
            {
                // Debug
                Debug.Log("Electricity Manager : Ouch");
                // Update To Attac so if Doom Shroom is waiting he will jump
                Electricity_Manager_St.Update_State(Boss_States.Attack);
                Electricity_Manager_An.Launch_Stunt_Animation();
            }
            else
            {
                Electricity_Manager_St.Update_State(Boss_States.Dead);
                Electricity_Manager_An.Launch_Death_Animation();
            }

            HUD.Update_Boss_Health_Bar(Current_Health, Max_Health);
        }
    }
}

