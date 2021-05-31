using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Slime_Movement : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Spawn_Velocity_X = -3;
    [SerializeField]
    private float Spawn_Velocity_Y = 3;

    [SerializeField]
    private float Min_Distance = 0.3f;
    [SerializeField]
    private float Easy_Speed = 4f;
    [SerializeField]
    private float Normal_Speed = 5f;
    [SerializeField]
    private float Hard_Speed = 6.5f;
    [SerializeField]
    private Transform Ground_Check_Transform;
    [SerializeField]
    private float Ground_Check_Radius = 0.05f;
    // Variables 
    private Rigidbody2D Mini_Slime_RB;
    private GameObject Target;
    private bool Is_Alive = true;
    private bool Is_Grounded = false;
    private float Horizontal_Speed = 0f;
    private float Max_Horizontal_Speed;
    private Mini_Slime_Animation Mini_Slime_An;
    // Start is called before the first frame update
    void Start()
    {
        Mini_Slime_RB = GetComponent<Rigidbody2D>();
        Mini_Slime_RB.velocity = new Vector2(Spawn_Velocity_X, Spawn_Velocity_Y);
        Mini_Slime_An = GetComponent<Mini_Slime_Animation>();
        Target = GameObject.Find("Player");
        Debug.Log(Target);
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
    }

    // Update is called once per frame
    void Update()
    {
        // Check player contact with ground 
        Is_Grounded = Physics2D.OverlapCircle(Ground_Check_Transform.position, Ground_Check_Radius, LayerMask.GetMask("Ground"));
        if (Is_Grounded)
        {
            Mini_Slime_An.Launch_Ground_Animaion();
            if (Is_Alive)
            {
                Move_To_Target(Target.transform);
            }
            else
            {
                Mini_Slime_RB.velocity = new Vector2(0, Mini_Slime_RB.velocity.y);
            }
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
        }
        if (Distance_From_Target_X < Min_Distance)
        {
            Horizontal_Speed = 0;
        }
        // Set Rigid Body velocity to move the mob
        Mini_Slime_RB.velocity = new Vector2(Horizontal_Speed * Max_Horizontal_Speed, Mini_Slime_RB.velocity.y);
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(Min_Distance, 15, 0));
        Gizmos.DrawWireSphere(Ground_Check_Transform.position, Ground_Check_Radius);
    }
    
    public void Set_Life(bool Alive)
    {
        Is_Alive = Alive;
    }
    public bool Get_Life()
    {
        return Is_Alive;
    }
    
}
