using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB_Slime_Movement : MonoBehaviour
{
    // Parameters
    [SerializeField]
    private float Easy_Speed = 2f;
    [SerializeField]
    private float Normal_Speed = 3f;
    [SerializeField]
    private float Hard_Speed = 4f;
    [SerializeField]
    private Transform Left_Limit;
    [SerializeField]
    private Transform Right_Limit;
    [SerializeField]
    private float Limit_Min_Distance = 1f;
    // Variables
    private float Horizontal_Speed;
    private Boss_States State = Boss_States.Cinematic;
    private bool Go_To_Left = true;
    private Rigidbody2D RGB_Slime_RB;
    private float Max_Horizontal_Speed;
    private BoxCollider2D Hit_Box;
    private bool Is_Frozen = false;
    // Start is called before the first frame update
    void Start()
    {
        // Switch RGB Slime Speed accordingly to the global difficulty
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
        Left_Limit.parent = null;
        Right_Limit.parent = null;
        RGB_Slime_RB = GetComponent<Rigidbody2D>();
        Hit_Box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If RGB_Slime is able to move
        if (!Is_Frozen)
        {
            // Check if the boss is in a state of potential movement
            if (!(State == Boss_States.Cinematic || State == Boss_States.Dead))
            {
                if (Go_To_Left)
                {
                    Move_To_Target(Left_Limit);
                }
                else
                {
                    Move_To_Target(Right_Limit);
                }
            }
            else
            {
                RGB_Slime_RB.velocity = new Vector2(0, RGB_Slime_RB.velocity.y);
            }
        }
    }
    private void Move_To_Target(Transform target_transform)
    {
        // Get horizonatal distance form target
        float Distance_From_Target_X = Mathf.Abs(transform.position.x - target_transform.transform.position.x);

        // Check if the target is in reach an not to close to prevent this mob to "freak out" and set the speed in direction of target
        if (Distance_From_Target_X > Limit_Min_Distance)
        {
            if (target_transform.transform.position.x > this.transform.position.x)
            {
                Horizontal_Speed = 2;
            }
            else
            {
                if (target_transform.transform.position.x < this.transform.position.x)
                {
                    Horizontal_Speed = -1;
                }
            }
            
        }
        if (Distance_From_Target_X < Limit_Min_Distance)
        {
            // Used to change target
            Change_Spawn_Direction();
            // Freeze the mob
            Horizontal_Speed = 0;
        }
        // Set Rigid Body velocity to move the mob
        RGB_Slime_RB.velocity = new Vector2(Horizontal_Speed * Max_Horizontal_Speed, RGB_Slime_RB.velocity.y);
    }
    public void Change_Spawn_Direction()
    {
        Go_To_Left = !Go_To_Left;
        Debug.Log("Direction changed");
    }


    // Update state when called
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Mouvement States = " + State);
    }
    public void Freeze()
    {
        Is_Frozen = false;
        // Freeze position
        RGB_Slime_RB.velocity = new Vector2(0, 0);
        // Disble gravity to prevent falling in the void
        RGB_Slime_RB.gravityScale = 0;
        // Disable hitbox collision
        Hit_Box.isTrigger = true;
        Debug.Log(" Frozen " + RGB_Slime_RB.velocity + " " + RGB_Slime_RB.gravityScale + " " + Hit_Box.isTrigger);
    }
}
