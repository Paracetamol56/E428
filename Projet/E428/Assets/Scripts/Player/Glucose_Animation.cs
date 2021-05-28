using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose_Animation : MonoBehaviour
{
    // Parameters

    // Private Variables
    private Rigidbody2D Player_RB;
    private SpriteRenderer Player_SR;
    private Animator Player_An;
    private Glucose_Controls Player_Mo;
    [HideInInspector]
    public bool Is_Facing_Right = false;
    private float Horizontal_Velocity = 0;
    private float Vertical_Velocity = 0;
    private bool Is_Dead = false;
    // Start is called before the first frame update
    void Start()
    {
        // Gameobjects Components getters
        Player_RB = GetComponent<Rigidbody2D>();
        Player_SR = GetComponent<SpriteRenderer>();
        Player_An = GetComponent<Animator>();
        Player_Mo = GetComponent<Glucose_Controls>();
        Change_Animation_Vibe(Glucose_States.Player_Emotion.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Is_Dead)
        {
            // Get player  velocity
            Horizontal_Velocity = Player_RB.velocity.x;
            Vertical_Velocity = Player_RB.velocity.y;
            // Flip sprite in the direction the player facing
            if (Is_Facing_Right && Horizontal_Velocity < 0)
                Look_Left();
            else if (!Is_Facing_Right && Horizontal_Velocity > 0)
                Look_Right();

            // Update Animator values
            Player_An.SetFloat("Velocity_X", Mathf.Abs(Horizontal_Velocity));
            Player_An.SetFloat("Velocity_Y", Vertical_Velocity);
            Player_An.SetBool("Is_Grounded", Player_Mo.Get_Is_Grounded());
        }
        
    }
    // Flip sprite in the direction the player is facing
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Player_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Player_SR.flipX = false;
    }
    public void Change_Animation_Vibe(Glucose_States.Player_Emotion Emotion)
    {
        Player_An.SetFloat("Emotion", (int)Emotion);
    }
    public void Change_Animation_Control(Glucose_States.Player_Control Control)
    {
        // Make Player invisible when piped
        Player_SR.enabled = (Control != Glucose_States.Player_Control.Pipe);
    }
    public void Launch_Attack_Animation()
    {
        Player_An.SetTrigger("Attack");
    }
    public void Launch_Attack_Release_Animation()
    {
        Player_An.SetTrigger("Release");
    }
    public void Launch_Death_Animation()
    {
        Is_Dead = true;
        Player_An.SetBool("Is_Dead", true) ;
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Animation());
    }
    
    private IEnumerator Stunt_Animation()
    {
        Player_SR.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Player_SR.color = Color.white;
    }
}
