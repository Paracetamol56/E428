using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Slime_Animation : MonoBehaviour
{
    // Variable
    private Rigidbody2D Blue_Slime_An;
    private SpriteRenderer Blue_Slime_SR;
    private Animator Mob_An;
    private float Horizontal_Velocity = 0;

    private bool Is_Facing_Right = false;
    // Start is called before the first frame update
    void Start()
    {
        Blue_Slime_An = GetComponent<Rigidbody2D>();
        Blue_Slime_SR = GetComponent<SpriteRenderer>();
        Mob_An = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mob  velocity
        Horizontal_Velocity = Blue_Slime_An.velocity.x;
        // Flip sprite in the direction the mob facing
        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();

        Mob_An.SetFloat("Velocity_X", Mathf.Abs(Horizontal_Velocity));

    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Blue_Slime_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Blue_Slime_SR.flipX = false;
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Animation());
    }
    private IEnumerator Stunt_Animation()
    {
        Blue_Slime_SR.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Blue_Slime_SR.color = Color.white;
    }
    public void Launch_Death_Animation()
    {
        Mob_An.SetTrigger("Die");
    }
}
