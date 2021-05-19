using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mob_Basic_Movement : MonoBehaviour
{
    // Parameters 
    public float Max_View_Distance = 10;
    public float Min_Distance = 0.3f;
    public GameObject Target;
    public float Easy_Speed = 4f;
    public float Normal_Speed = 5f;
    public float Hard_Speed = 6.5f;
    public float Health_Easy = 50;
    public float Health_Normal = 70;
    public float Health_Hard = 80;
    // Variables
    private Rigidbody2D Mob_RB;
    private Transform Spawn_Point;
    private float Horizontal_Speed = 0f;
    private float Max_Horizontal_Speed;
    private bool Is_Agro = false;

    // Start is called before the first frame update
    void Start()
    {
        Spawn_Point = transform;
        Mob_RB = GetComponent<Rigidbody2D>();
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
        // Get distance from Target game object 
        float Distance_From_Target = Vector2.Distance(Target.transform.position, this.transform.position);
        // Get horizonatal distance form target
        float Distance_From_Target_X = Mathf.Abs(transform.position.x - Target.transform.position.x);


       // Check if the target is in reach distance from this mob
        if(Distance_From_Target < Max_View_Distance)
            Is_Agro = true;

        // Check if the target is in reach an not to close to prevent this mob to "freak out" and set the speed in direction of target
        if (Is_Agro && Distance_From_Target_X > Min_Distance)
        {
            if (Target.transform.position.x > this.transform.position.x)
            {
                Horizontal_Speed = 1;
            }
            else
            {
                if (  Target.transform.position.x < this.transform.position.x)
                {
                    Horizontal_Speed = -1;
                }
            }
        }
        if(Distance_From_Target_X < Min_Distance)
        {
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
}
