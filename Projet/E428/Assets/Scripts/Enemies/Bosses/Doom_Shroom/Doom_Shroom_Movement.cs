using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Movement : MonoBehaviour
{
    // Parameters
    [SerializeField]
    private float Easy_Jump_Delay = 2f;
    [SerializeField]
    private float Normal_Jump_Delay = 3f;
    [SerializeField]
    private float Hard_Jump_Delay = 4f;
    [SerializeField]
    private float Attack_Lenght = 2f;
    [SerializeField]
    private float Jump_Horizontal_Speed = 5f;
    [SerializeField]
    private float Jump_Vertical_Speed = 5f;
    [SerializeField]
    private BoxCollider2D Ground_Checker;
    // Variables
    private float Current_Jump_Delay;
    private Boss_States State = Boss_States.Attack;
    private bool Go_To_Left = true;
    private Rigidbody2D Doom_Shroom_RB;
    private BoxCollider2D Hit_Box;
    private Doom_Shroom_Animation Doom_Shroom_An;
    private bool Is_Frozen = false;
    private bool Is_Grounded = true;
    // Start is called before the first frame update
    void Start()
    {
        // Switch Doom Shroom Delay Speed accordingly to the global difficulty
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
        Hit_Box = GetComponent<BoxCollider2D>();
        Doom_Shroom_RB = GetComponent<Rigidbody2D>();
        Doom_Shroom_An = GetComponent<Doom_Shroom_Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Doom_Shroom is able to move and grounded
        if (Is_Grounded)
        {
            // Check if the boss is in a state of potential movement
            if (!(State == Boss_States.Cinematic || State == Boss_States.Dead))
            {
                Jump(Go_To_Left);
            }
            Debug.Log("Doom Shroom velocity = " + Doom_Shroom_RB.velocity);
        }
    }
    public void Jump(bool left)
    {
        StartCoroutine(Jump_Action(left));
    }
    private IEnumerator Jump_Action(bool left)
    {
        // Stop potential jumps
        Is_Grounded = false;
        Doom_Shroom_RB.gravityScale = 0.3f;
        // Launch animation
        Doom_Shroom_An.Launch_Attack_Animation();
        // Wait for attack lenth
        yield return new WaitForSeconds(Attack_Lenght);
        // Launch animation
        Doom_Shroom_An.Launch_Jump_Animation();
        // Move jump trigger inside th collision to block ground check while we wait for the next windows of action
        Ground_Checker.transform.localPosition = new Vector3(0, 0, 0);
        // If Doom_Shroom has to jump to the left go in the left direction or go to the right direction if not
        if (left)
            Doom_Shroom_RB.velocity = new Vector2(-Jump_Horizontal_Speed, Jump_Vertical_Speed);
        else
            Doom_Shroom_RB.velocity = new Vector2(Jump_Horizontal_Speed, Jump_Vertical_Speed);
        yield return new WaitForSeconds(1.5f );
        Doom_Shroom_RB.gravityScale = 3f;

        yield return new WaitForSeconds(Current_Jump_Delay);
        // Move jump trigger outside the collision to make ground check possible
        Ground_Checker.transform.localPosition = new Vector3(0, -2, 0);
    }
    public void Change_Direction()
    {
        Go_To_Left = !Go_To_Left;
    }

    public void Set_Grounded(bool grounded)
    {
        Is_Grounded = grounded;
    }
    public void Freeze()
    {
        //Is_Frozen = true;
        //// Freeze position
        //Doom_Shroom_RB.velocity = new Vector2(0, 0);
        //// Disble gravity to prevent falling in the void
        //Doom_Shroom_RB.gravityScale = 0;
        //// Disable hitbox collision
        //Hit_Box.isTrigger = true;
        //Debug.Log(" Frozen " + Doom_Shroom_RB.velocity + " " + Doom_Shroom_RB.gravityScale + " " + Hit_Box.isTrigger);
    }
    // Update state when called
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Mouvement States = " + State);
    }
}
