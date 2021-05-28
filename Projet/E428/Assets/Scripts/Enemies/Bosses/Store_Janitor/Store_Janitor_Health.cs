using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_Health : MonoBehaviour, IAttackable
{
    // Parametters
    // Health
    // Parametters


    [SerializeField]
    private int Health_Easy = 7 ;
    [SerializeField]
    private int Health_Normal = 8;
    [SerializeField]
    private int Health_Hard = 10;
    // Variables
    private HUD_Manager HUD;
    private int Max_Health;
    private int Current_Health;
    private Store_Janitor_Animation Store_Janitor_An;
    private Store_Janitor_States Store_Janitor_St;
    private Store_Janitor_Attack Store_Janitor_At;

    // Start is called before the first frame update
    void Start()
    {
        
        Store_Janitor_An = GetComponent<Store_Janitor_Animation>();
        Store_Janitor_St = GetComponent<Store_Janitor_States>();
        Store_Janitor_At = GetComponent<Store_Janitor_Attack>();
        HUD = Store_Janitor_St.HUD;
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
        Current_Health -= 1;
        // Make boss angrier
        Store_Janitor_At.Increase_Difficulty();
        // If the boss has heath, he become stunt else he die
        if (Current_Health > 0)
        {
           
            Store_Janitor_An.Launch_Stunt_Animation();
        }
        else
        {
            Debug.Log("Store Janitor is Dead= ");
            Store_Janitor_St.Update_State(Boss_States.Dead);
            Store_Janitor_An.Launch_Death_Animation();
        }
        Debug.Log("Store Janitor Health = " + Current_Health);
        HUD.Update_Boss_Health_Bar(Current_Health, Max_Health);
    }
}
