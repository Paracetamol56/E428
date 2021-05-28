using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_Mouvements : MonoBehaviour
{
    // Parametters


    // Speed
    [SerializeField]
    private float Easy_Speed = 4f;
    [SerializeField]
    private float Normal_Speed = 5f;
    [SerializeField]
    private float Hard_Speed = 6.5f;
    
    // Public
    [HideInInspector]
    public bool Go_To_Left = true;


    // Variable
    private Rigidbody2D Store_Janitor_RB;
    private Boss_States State = Boss_States.Attack;
    
    private float Max_Horizontal_Speed;
    private float Horizontal_Speed = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Store_Janitor_RB = GetComponent<Rigidbody2D>();
        // Switch difficulty depending on global variable
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Max_Horizontal_Speed = Hard_Speed;
                break;
            case 1:
                Max_Horizontal_Speed = Normal_Speed;
                break;
            default:
                Max_Horizontal_Speed = Easy_Speed;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the boss is in attack state and
        if (State == Boss_States.Attack)
        {
            if (Go_To_Left)
            {
                Horizontal_Speed = -Max_Horizontal_Speed;
            }
            else
            {
                Horizontal_Speed = Max_Horizontal_Speed;
            }
            Store_Janitor_RB.velocity = new Vector2(Horizontal_Speed, Store_Janitor_RB.velocity.y);
        }
        else
        {
            Store_Janitor_RB.velocity = new Vector2(0, 0);
        }
    }
    // Update state when called
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Store Janitor Mo state = " + State);
    }
    
}
