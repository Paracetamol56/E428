using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Glucose_Attack : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private const float Powered_Attack_Min_Time = .5f;
    [SerializeField]
    private const float Heavy_Attack_Min_Time = 1f;
    [SerializeField]
    private GameObject Spit;
    // Variables
    private Glucose_Animation Glucose_An;
    private Glucose_States.Player_Control Glucose_Control = Glucose_States.Player_Control.Normal;
    private bool Is_Attack_Hold;
    private float Hold_Time_Start = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Glucose_An = GetComponent<Glucose_Animation>();
    }


    public void GetAttackInput(InputAction.CallbackContext context)
    {
            Glucose_An.Launch_Attack_Animation();
            // Transfer from event to variable
            if (Glucose_Control == Glucose_States.Player_Control.Normal)
            {
                // Check if the attack button is Pressed
                if (context.started)
                {
                    Is_Attack_Hold = true;
                    Hold_Time_Start = Time.time;
                }
                else if (context.canceled)
                {
                    Is_Attack_Hold = false;
                    Attack_Action(Time.time - Hold_Time_Start);
                }
            }
    }
    public void Attack_Action(float Hold_Time)
    {

        if (Hold_Time < Powered_Attack_Min_Time)
        {
            print(Hold_Time);
            print("Failed Attack");
           
        }
        else
        {

            if (Hold_Time > Heavy_Attack_Min_Time)
            {

                print("Heavy Attack");
                if (!Glucose_An.Is_Facing_Right)
                {
                    Instantiate(Spit, transform.position, Quaternion.Euler(0, 0, 0));
                    Instantiate(Spit, transform.position, Quaternion.Euler(0, 0, -15));
                }
                else
                {
                    Instantiate(Spit, transform.position, Quaternion.Euler(0, 180, 0));
                    Instantiate(Spit, transform.position, Quaternion.Euler(0, 180, -15));
                }
            }
            else
            {
                print("Powered Attack");
                
                if (!Glucose_An.Is_Facing_Right)
                    Instantiate(Spit, transform.position, Quaternion.Euler(0, 0, -5));
                else
                    Instantiate(Spit, transform.position, Quaternion.Euler(0, 180, -5));
            }
        }
        Glucose_An.Launch_Attack_Release_Animation();
    }
    public void Toggle_Attack(Glucose_States.Player_Control control)
    {
        Glucose_Control = control;
    }
}
