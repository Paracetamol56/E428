using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Animation : MonoBehaviour
{

    // Variables
    private Animator Doom_Shroom_An;
    private Rigidbody2D Doom_Shroom_RB;
    private SpriteRenderer Doom_Shroom_SR;
    private bool Is_Facing_Right = false;
    private float Horizontal_Velocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        Doom_Shroom_An = GetComponent<Animator>();
        Doom_Shroom_RB = GetComponent<Rigidbody2D>();
        Doom_Shroom_SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal_Velocity = Doom_Shroom_RB.velocity.x;

        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();
    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Doom_Shroom_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Doom_Shroom_SR.flipX = false;
    }
    public void Launch_Jump_Animation()
    {
        Doom_Shroom_An.SetTrigger("Jump");
    }
    public void Launch_Death_Animation()
    {
        Doom_Shroom_An.SetBool("Death",true);
    }
    public void Launch_Attack_Animation()
    {
        Doom_Shroom_An.SetTrigger("Attack");
    }
}
