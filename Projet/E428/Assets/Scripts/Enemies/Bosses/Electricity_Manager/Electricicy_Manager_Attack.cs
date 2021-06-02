using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricicy_Manager_Attack : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private GameObject Attack_To_Spawn;
    [SerializeField]
    private float Easy_Attack_Delay = 2f;
    [SerializeField]
    private float Normal_Attack_Delay = 1.5f;
    [SerializeField]
    private float Hard_Attack_Delay = 1f;
    // Variables
    private float Current_Attack_Delay;
    private bool Can_Attack = true;
    private Boss_States State = Boss_States.Cinematic;
    private Electricity_Manager_Animation Electricity_Manager_An;
    // Start is called before the first frame update
    void Start()
    {
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Attack_Delay = Hard_Attack_Delay;
                break;
            case 1:
                Current_Attack_Delay = Normal_Attack_Delay;
                break;
            default:
                Current_Attack_Delay = Easy_Attack_Delay;
                break;
        }
        Electricity_Manager_An = GetComponent<Electricity_Manager_Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        // If boss is alive and is not already attacking
        if (State == Boss_States.Attack && Can_Attack)
        {
            StartCoroutine(Attack_Action());
        }
        
    }
    private IEnumerator Attack_Action()
    {
        Can_Attack = false;
        yield return new WaitForSeconds(Current_Attack_Delay);
        if (State != Boss_States.Dead)
        {
            // Launch Cast Animation
            Electricity_Manager_An.Launch_Cast_Animation();
            Instantiate(Attack_To_Spawn, Target.transform.position, Target.transform.rotation, null);
            Can_Attack = true;
        }
    }
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Electricity Manager Att state = " + State);
    }
}
