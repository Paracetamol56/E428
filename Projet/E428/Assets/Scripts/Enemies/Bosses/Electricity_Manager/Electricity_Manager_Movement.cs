using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity_Manager_Movement : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Easy_Speed = 4f;
    [SerializeField]
    private float Normal_Speed = 5f;
    [SerializeField]
    private float Hard_Speed = 6.5f;
    [SerializeField]
    private float Fly_Amplitude = 3f;
    // Variables
    private float Current_Speed;
    private Vector3 Spawn_Position;
    private Boss_States State = Boss_States.Attack;
    private float Raw_Y_Potential_Position = 0;
    // Start is called before the first frame update
    void Start()
    {
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Speed = Hard_Speed;
                break;
            case 1:
                Current_Speed = Normal_Speed;
                break;
            default:
                Current_Speed = Easy_Speed;
                break;
        }
        // Get the spawn transition
        Spawn_Position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Add To the potential position 
        
        if (State == Boss_States.Attack)
        {
            Raw_Y_Potential_Position += Current_Speed * Time.deltaTime;
            //  Current Y position is equal to cos of Raw_Y_Potential_Position mapped to 0 to 1 muliplied by the amplitude + th og y position
            float Current_Y_Position = (Mathf.Cos(Raw_Y_Potential_Position) / 2 + 0.5f)* Fly_Amplitude + Spawn_Position[1];
            transform.position = new Vector3(transform.position.x, Current_Y_Position, 0);
        }
    }
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Electricity Manager Mo state = " + State);
    }
}
