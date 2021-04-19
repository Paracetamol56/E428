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
    private bool Is_Facing_Right = false;
    private float Horizontal_Velocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Gameobjects Components getters
        Player_RB = GetComponent<Rigidbody2D>();
        Player_SR = GetComponent<SpriteRenderer>();
        Player_An = GetComponent<Animator>();
        Change_Animation_Vibe(Glucose_States.Player_Emotion.Curious);
    }

    // Update is called once per frame
    void Update()
    {
        // Get player horizontal velocity
        Horizontal_Velocity = Player_RB.velocity.x;
        // Flip sprite in the direction the player facing
        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();
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
        Player_An.SetFloat("Emotion",(int) Emotion);
    }
    public void Change_Animation_Control(Glucose_States.Player_Control Control)
    {
        // Make Player invisible when piped

        Player_SR.enabled = (Control != Glucose_States.Player_Control.Pipe);
    }
}
