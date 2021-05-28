using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_Wait : MonoBehaviour
{
    // Parametters
    // Wait time
    [SerializeField]
    private float Easy_Wait_Time = 5f;
    [SerializeField]
    private float Normal_Wait_Time = 4f;
    [SerializeField]
    private float Hard_Wait_Time = 3.5f;

    
    // Variables 
    private Store_Janitor_Mouvements Store_Janitor_Mo;
    private Store_Janitor_States Store_Janitor_St;
    private float Wait_Time;

    private void Start()
    {
        Store_Janitor_Mo = GetComponentInParent<Store_Janitor_Mouvements>();
        Store_Janitor_St = GetComponentInParent<Store_Janitor_States>();
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Wait_Time = Hard_Wait_Time;
                break;
            case 1:
                Wait_Time = Normal_Wait_Time;
                break;
            default:
                Wait_Time = Easy_Wait_Time;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

            // Start Waiting
            StartCoroutine(Wait_After_Attack());
            // Switch Janitor direction
            Store_Janitor_Mo.Go_To_Left = !Store_Janitor_Mo.Go_To_Left;
    }
    IEnumerator Wait_After_Attack()
    {
        Store_Janitor_St.Update_State(Boss_States.Wait);
        yield return new WaitForSeconds(Wait_Time);
        Store_Janitor_St.Update_State(Boss_States.Attack);
    }
}
