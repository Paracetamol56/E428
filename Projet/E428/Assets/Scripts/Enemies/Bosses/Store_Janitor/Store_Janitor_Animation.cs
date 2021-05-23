using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Janitor_Animation : MonoBehaviour
{    // Variables
    private Rigidbody2D Store_Janitore_RB;
    private SpriteRenderer Store_Janitore_SR;
    private Animator Store_Janitore_An;
    private float Horizontal_Velocity = 0;
    private bool Is_Facing_Right = false;
    // Start is called before the first frame update
    void Start()
    {
        Store_Janitore_RB = GetComponent<Rigidbody2D>();
        Store_Janitore_SR = GetComponent<SpriteRenderer>();
        Store_Janitore_An = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mob  velocity
        Horizontal_Velocity = Store_Janitore_RB.velocity.x;
        // Flip sprite in the direction the mob facing
        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();

        Store_Janitore_An.SetFloat("Velocity_X", Mathf.Abs(Horizontal_Velocity));

    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Store_Janitore_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Store_Janitore_SR.flipX = false;
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Color());
    }
    IEnumerator Stunt_Color()
    {
        print("Stunt");
        Store_Janitore_SR.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Store_Janitore_SR.color = Color.white;
    }
    public void Launch_Death_Animation()
    {
        Store_Janitore_An.SetTrigger("Death");

    }
}

