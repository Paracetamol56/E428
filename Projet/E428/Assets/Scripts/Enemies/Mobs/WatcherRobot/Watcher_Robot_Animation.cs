using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher_Robot_Animation : MonoBehaviour
{
    // Variable
    private Rigidbody2D Watcher_RB;
    private SpriteRenderer Watcher_SR;
    private Animator Watcher_An;
    private float Horizontal_Velocity = 0;

    private bool Is_Facing_Right = false;
    // Start is called before the first frame update
    void Start()
    {
        Watcher_RB = GetComponent<Rigidbody2D>();
        Watcher_SR = GetComponent<SpriteRenderer>();
        Watcher_An = GetComponent<Animator>();
    }
    void Update()
    {
        // Get mob  velocity
        Horizontal_Velocity = Watcher_RB.velocity.x;
        // Flip sprite in the direction the mob facing
        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();

    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Watcher_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Watcher_SR.flipX = false;
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Animation());
    }
    private IEnumerator Stunt_Animation()
    {
        Watcher_SR.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Watcher_SR.color = Color.white;
    }
    public void Launch_Death_Animation()
    {
        Watcher_An.SetTrigger("Die");
    }
    public void Launch_Attack_Animation()
    {
        Watcher_An.SetTrigger("Attack");
    }
}
