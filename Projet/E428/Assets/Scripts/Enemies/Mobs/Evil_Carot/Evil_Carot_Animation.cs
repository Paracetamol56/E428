using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil_Carot_Animation : MonoBehaviour
{
    private Animator Evil_Carot_An;
    private SpriteRenderer Evil_Carot_Sr;
    private Rigidbody2D Evil_Carot_Rb;
    private bool Is_Facing_Right = false;
    private float Horizontal_Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Evil_Carot_An = GetComponent<Animator>();
        Evil_Carot_Sr = GetComponent<SpriteRenderer>();
        Evil_Carot_Rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Horizontal_Velocity = Evil_Carot_Rb.velocity.x;
        // Flip sprite in the direction the mob facing
        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();

    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Evil_Carot_Sr.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Evil_Carot_Sr.flipX = false;
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Animation());
    }
    private IEnumerator Stunt_Animation()
    {
        Evil_Carot_Sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Evil_Carot_Sr.color = Color.white;
    }


    public void Jump()
    {
        Evil_Carot_An.SetTrigger("Jump");
    }
    public void Launch_Death_Animation()
    {
        Evil_Carot_An.SetTrigger("Die");
    }
}
