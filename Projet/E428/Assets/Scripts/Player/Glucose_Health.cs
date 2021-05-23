using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose_Health : MonoBehaviour,IAttackable
{
    // Parametters
    [SerializeField]
    private int Max_Health = 7;
    [SerializeField]
    private float Attack_Delay = 0.5f;
    [SerializeField]
    private HUD_Manager HUD;
    // Variables
    private int Current_Health;
    private Glucose_Animation Glucose_An;
    private float Next_Attack = 0;
    private Glucose_States Glucose_St;
    private Glucose_Mouvements Glucose_Mo;

    // Start is called before the first frame update
    void Start()
    {
        // Get all components
        Glucose_An = GetComponent<Glucose_Animation>();
        Glucose_St = GetComponent<Glucose_States>();
        Glucose_Mo = GetComponent<Glucose_Mouvements>();
        // Setup Current Health
        Current_Health = Max_Health;
    }
    public void Be_Attacked()
    {
        if (Next_Attack < Time.time)
        {
            Current_Health--;
            Next_Attack = Time.time + Attack_Delay;
            if (Current_Health > 0)
            {
                Glucose_An.Launch_Stunt_Animation();
                Glucose_Mo.Stunt();
            }
            else
            {
                print("Health Dead");
                Glucose_St.Change_Glucose_Controls(Glucose_States.Player_Control.Dead);
            }
        }
       
        HUD.Update_Glucose_Health_Bar(Current_Health, Max_Health);
    }
}
