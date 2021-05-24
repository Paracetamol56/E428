using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Slime_Movement : MonoBehaviour
{
    // Parameters 
    // Parameters 
    [SerializeField]
    private float Max_View_Distance = 10;
    [SerializeField]
    private float Min_Distance = 0.3f;
    public GameObject Target;
    [SerializeField]
    private float Easy_Speed = 4f;
    [SerializeField]
    private float Normal_Speed = 5f;
    [SerializeField]
    private float Hard_Speed = 6.5f;
    [SerializeField]
    private Transform Spawn_Point_Left;
    [SerializeField]
    private Transform Spawn_Point_Right;
    // Variables

    private Rigidbody2D Mob_RB;

    private float Horizontal_Speed = 0f;
    private float Max_Horizontal_Speed;
    private bool Is_Agro = false;
    private bool Is_Alive = true;
    private bool Go_To_left = false;
    // Start is called before the first frame update

    void Start()
    {
        // Unparent spawn points to make them not move
        Spawn_Point_Left.parent = null;
        Spawn_Point_Right.parent = null;

         Mob_RB = GetComponent<Rigidbody2D>();
        // Difficulty switch
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
            Is_Agro = true;
        if (Is_Alive)
        {
            if (Is_Agro)
            {// Move to player
                Move_To_Target(Target.transform);
            }
            else
            { // Move to spawn Point
                // If the mob is already at destination change destination
                    
                // Go to the chosen destination
                if (Go_To_left)
                {
                    Move_To_Target(Spawn_Point_Left);
                }
                else
                {
                    Move_To_Target(Spawn_Point_Right);
                }

            }
        }
        else
        {
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
            // Used to change target
            Change_Spawn_Direction();
            // Freeze the mob
            Horizontal_Speed = 0;
        }
        // Set Rigid Body velocity to move the mob
        Mob_RB.velocity = new Vector2(Horizontal_Speed * Max_Horizontal_Speed, Mob_RB.velocity.y);
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
    public void Change_Spawn_Direction()
    {
        Go_To_left = !Go_To_left;
        print("Direction changed");
        print(Go_To_left);
    }
}
