using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_Attack : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Easy_Time_Between_Launch = 1.5f;
    [SerializeField]
    private float Normal_Time_Between_Launch = 1f;
    [SerializeField]
    private float Hard_Time_Between_Launch = 0.5f;
    [SerializeField]
    private GameObject TNT;
    // Variables
    private float Time_Between_Launch;
    private float Next_Lauch = 0;
    
    private Boss_States State = Boss_States.Attack;
    // Start is called before the first frame update
    void Start()
    {
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Time_Between_Launch = Hard_Time_Between_Launch;
                break;
            case 1:
                Time_Between_Launch = Normal_Time_Between_Launch;
                break;
            default:
                Time_Between_Launch = Easy_Time_Between_Launch;
                break;
        }
    }
    private void Update()
    {
        
        if (State == Boss_States.Attack && Time.time > Next_Lauch)
        {
            Next_Lauch = Time.time + Time_Between_Launch;
            Instantiate(TNT, transform);
        }
    }


    // Update is called once per frame

   
    // Update states
    public void Update_State(Boss_States state)
    {
        State = state;
    }
    // Decrase the time between launches
    public void Increase_Difficulty()
    {
        print("Diff++");
        Time_Between_Launch *= 0.95f;
    }
}
