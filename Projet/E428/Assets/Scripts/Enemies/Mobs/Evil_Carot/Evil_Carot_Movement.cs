using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil_Carot_Movement : MonoBehaviour
{
    // Parameters 
    // Parameters 
    [SerializeField]
    private float Max_View_Distance = 10;
    [SerializeField]
    private float Min_Distance = 0.3f;
    public GameObject Target;
    [SerializeField]
    private float Easy_Jump_Delay = 1f;
    [SerializeField]
    private float Normal_Jump_Delay = 0.5f;
    [SerializeField]
    private float Hard_Jump_Delay = 0.25f;
    [SerializeField]
    private float Jump_Strenght = 13;

    // Variables
    private Evil_Carot_Animation Evil_Carot_An;
    private Rigidbody2D Mob_RB;
    private float Horizontal_Speed = 0f;
    private float Current_Jump_Delay;
    private Transform Spawn_Point;
    private bool Is_Agro = false;
    private bool Is_Alive = true;
    private bool Can_Jump = true;
    private float Next_Jump_Time;
    // Start is called before the first frame update

    void Start()
    {
        Evil_Carot_An = GetComponent<Evil_Carot_Animation>();
        Mob_RB = GetComponent<Rigidbody2D>();
        // Get Spawn_Point
        Spawn_Point = transform;
        // Difficulty switch
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Jump_Delay = Hard_Jump_Delay;
                break;
            case 1:
                Current_Jump_Delay = Normal_Jump_Delay;
                break;
            default:
                Current_Jump_Delay = Easy_Jump_Delay;
                break;
        }
        // Pipe event subscription
        Event_System.current.onPipeEntered += Piped;
    }
    private void OnDestroy()
    {
        // Pipe event unsubsrciption 
        Event_System.current.onPipeEntered -= Piped;
    }
    // Update is called once per frame
    void Update()
    {
        // Get distance from Target game object 
        float Distance_From_Target = Vector2.Distance(Target.transform.position, this.transform.position);

        // Check if the target is in reach distance from this mob
        if (Distance_From_Target < Max_View_Distance)
        {
            Is_Agro = true;
        }
        else
            Is_Agro = false;

        if (Is_Alive)
        {
            if (Is_Agro)
            {// Move to player
                Move_To_Target(Target.transform);
            }
            else
            { // Move to spawn Point
                Move_To_Target(Spawn_Point.transform);
            }
        }
        else
        {
            // When mob is dead
            Mob_RB.velocity = new Vector2(0, Mob_RB.velocity.y);
        }


    
}
    private void Move_To_Target(Transform target_transform)
    {
        // Get horizonatal distance form target
        float Distance_From_Target_X = Mathf.Abs(transform.position.x - target_transform.transform.position.x);

        // Check if the target is in reach an not to close to prevent this mob to "freak out" and set the speed in direction of target
        if (Distance_From_Target_X > Min_Distance)
        {
            if (target_transform.transform.position.x > this.transform.position.x)
            {
                Horizontal_Speed = 1;
            }
            else
            {
                if (target_transform.transform.position.x < this.transform.position.x)
                {
                    Horizontal_Speed = -1;
                }
            }
            if (Is_Agro) // If the mob is angry
            {
                Horizontal_Speed *= 1.5f; // Make it little faster
            }
        }
        if (Distance_From_Target_X < Min_Distance)
        {
            Horizontal_Speed = 0;
        }

        // Jump Periodicly when The mob is agro
        if (Next_Jump_Time < Time.time && Can_Jump && Is_Agro)
        {
            // Remove the abillity to jump
            Can_Jump = false;

            // Jump
            Mob_RB.velocity = new Vector2(Horizontal_Speed * Jump_Strenght / 3, Jump_Strenght);
            // Launch jump animation
            Evil_Carot_An.Jump();
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, Max_View_Distance);
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, Min_Distance);
        
        Gizmos.DrawWireCube(this.transform.position, new Vector3(Min_Distance, 15, 0));
    }
    public void Set_Agro(bool Agro)
    {
        Is_Agro = Agro;
    }
    public void Piped()
    {
        Is_Agro = false;
    }
    public void Set_Life(bool Alive)
    {
        Is_Alive = Alive;
    }
    public void Ground_Control()
    {
        Debug.Log("Ground_Control");
        Next_Jump_Time = Time.time + Current_Jump_Delay;
        Can_Jump = true;
    }
}
