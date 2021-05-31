using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Slime_Animation : MonoBehaviour
{
    // Variable
    private Rigidbody2D Mob_RB;
    private SpriteRenderer Mob_SR;
    private Animator Mob_An;
    private float Horizontal_Velocity = 0;

    private bool Is_Facing_Right = false;
    // Start is called before the first frame update
    void Start()
    {
        Mob_RB = GetComponent<Rigidbody2D>();
        Mob_SR = GetComponent<SpriteRenderer>();
        Mob_An = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mob  velocity
        Horizontal_Velocity = Mob_RB.velocity.x;
        // Flip sprite in the direction the mob facing
        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();

    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Mob_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Mob_SR.flipX = false;
    }
    public void Launch_Death_Animation()
    {
        Mob_An.SetTrigger("Die");
    }
    public void Launch_Ground_Animaion()
    {
        Mob_An.SetTrigger("Is_On_The_Ground");
    }
}
