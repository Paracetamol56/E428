using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Glucose_Attack : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private const float Powered_Attack_Min_Time = 0.25f;
    [SerializeField]
    private const float Heavy_Attack_Min_Time = 0.5f;
    // Variables
    private bool Is_Attack_Hold;
    private Glucose_Animation Glucose_An;
    private Glucose_States.Player_Control Glucose_Control = Glucose_States.Player_Control.Normal;

    private float Hold_Time_Start = 0;

    // Start is called before the first frame update
    void Start()
    {
        Glucose_An = GetComponent<Glucose_Animation>();
    }


    public void GetAttackInput(InputAction.CallbackContext context)
    {
        // Transfer from event to variable
        if ((int)Glucose_Control == 1)
        {
            // Check if the jump button is Pressed
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
        Glucose_An.Launch_Attack_Animation();
        switch (Hold_Time)
        {
            case Heavy_Attack_Min_Time:
                print("Heavy Attack");
                print(Hold_Time);   
                break;
            case Powered_Attack_Min_Time:
                print("Powered Attack");
                print(Hold_Time);
                break;
            default:
                print("Attack");
                print(Hold_Time);
                break;
        }
    }
}
