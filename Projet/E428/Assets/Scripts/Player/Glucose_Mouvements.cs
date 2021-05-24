using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Glucose_Mouvements : MonoBehaviour
{
    // Parameters
    [SerializeField]
    private float Horizontal_Speed = 1;
    [SerializeField]
    private float Horizontal_Acceleration = 1;
    [SerializeField]
    private float Jump_Force = 15;
    [SerializeField]
    private float Jump_Analog_Dampen = 0.5f;
    [SerializeField]
    private Transform Ground_Check_Transform;
    [SerializeField]
    private float Ground_Check_Radius = 0.05f;



    // Private variables

    // Horizontal input Vatriable
    private float Horizontal_Axis;

    // Player Physics
    private Rigidbody2D Player_RB;
    private bool Is_Grounded;

    // Jump Inputs
    private bool Is_Input_Jump_Pressed = false;
    private bool Is_Input_Jump_Hold = false;

    // Jump while falling time window
    private float Koyote_Time = 0.15f;
    private float Koyote_Counter;

    // Jump input before landing window
    private float Jump_Buffer_Lenght = 0.25f;
    private float Jump_Buffer_Count;

    // Time before another jump;
    private float Time_Before_Next_Jump;
    // Glucose control state
    private Glucose_States.Player_Control Glucose_Control = Glucose_States.Player_Control.Normal;
    // Glucose audio
    private Audio_Prefab_Spawner Audio_Prefab_Sp;
    // Stunt bool
    private bool Is_Stunt = false;
    // Start is called before the first frame update
    void Start()
    {
        Player_RB = GetComponent<Rigidbody2D>();
        Audio_Prefab_Sp = GetComponent<Audio_Prefab_Spawner>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Glucose_Control == Glucose_States.Player_Control.Normal && !Is_Stunt)
        {
            // Manage horizontal velocity
            Player_RB.AddForce(Vector2.right * Horizontal_Axis * Horizontal_Acceleration * 1000 * Time.deltaTime);
            // Clamp horizontal velocity
            if (Player_RB.velocity.x > Horizontal_Speed)
            {
                Player_RB.velocity = new Vector2(Horizontal_Speed, Player_RB.velocity.y);
            }
            if (Player_RB.velocity.x < -Horizontal_Speed)
            {
                Player_RB.velocity = new Vector2(-Horizontal_Speed, Player_RB.velocity.y);
            }

            // Check player contact with ground 
            Is_Grounded = Physics2D.OverlapCircle(Ground_Check_Transform.position, Ground_Check_Radius, LayerMask.GetMask("Ground"));

            // Manage Jump buffer (To make the player jump even if they pressed the jump button just before landing)
            if (Is_Input_Jump_Pressed)
            {
                Jump_Buffer_Count = Jump_Buffer_Lenght;
                Is_Input_Jump_Pressed = false;
            }
            else
            {
                Jump_Buffer_Count -= Time.deltaTime;
            }

            // Manage Koyote time (To make the player jump even if they pressed the jump button just after falling)
            if (Is_Grounded)
            {
                Koyote_Counter = Koyote_Time;
            }
            else
            {
                Koyote_Counter -= Time.deltaTime;
            }

            // Jump In the air (verify if the player has pressed jump before or after being on the ground and with a expeceted interval of time)
            if (Jump_Buffer_Count > 0 && Koyote_Counter > 0 && Time_Before_Next_Jump < 0)
            {
                Player_RB.AddForce(new Vector2(0, Jump_Force - Player_RB.velocity.y), ForceMode2D.Impulse);
                Jump_Buffer_Count = 0;
                
                // Set jump limiter to the time before next jump
                Time_Before_Next_Jump = Koyote_Time + 0.1f;
            }
            // Run next jump timer
            Time_Before_Next_Jump -= Time.deltaTime;
            if (Jump_Buffer_Count > 0)
            {
                Koyote_Counter = 0;
            }
            // Analog Jump
            if (!Is_Input_Jump_Hold && Player_RB.velocity.y > 0)
            {
                Player_RB.velocity = new Vector2(Player_RB.velocity.x, Player_RB.velocity.y * Jump_Analog_Dampen);
            }
        }
        else
        {
            Player_RB.velocity = new Vector2(0, Player_RB.velocity.y);
        }
    }
    // Get horizontal axis from in put package event
    public void GetHorizontalAxis(InputAction.CallbackContext context)
    {
        // Transfer from event to variable
        Horizontal_Axis = context.ReadValue<float>();
    }
    public void GetJump(InputAction.CallbackContext context)
    {
        if (Glucose_Control == Glucose_States.Player_Control.Normal)
        {
            if (context.started)
                Is_Input_Jump_Pressed = true;
            // Check if the jump button is Pressed
            if (context.started && !Is_Input_Jump_Hold)
            {
                Is_Input_Jump_Hold = true;
            }
            else if (context.canceled & Is_Input_Jump_Hold)
            {
                Is_Input_Jump_Hold = false;
            }
        }
    }
    // Gizmos helper
    private void OnDrawGizmosSelected()
    {
        // Ground Check Gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Ground_Check_Transform.position, Ground_Check_Radius);
    }
    public void Toggle_Movement(Glucose_States.Player_Control Control)
    {
        Glucose_Control = Control;
    }
    public bool Get_Is_Grounded()
    {
        return Is_Grounded;
    }
    // Used to stun momentarily Glucose
    public void Stunt()
    {
        StartCoroutine(Stunt_Time());
    }
    private IEnumerator Stunt_Time()
    {
        Is_Stunt = true;
        yield return new WaitForSeconds(0.1f);
        Is_Stunt = false;
    }
}
