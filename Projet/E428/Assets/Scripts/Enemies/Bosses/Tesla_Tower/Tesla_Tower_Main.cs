using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesla_Tower_Main : MonoBehaviour
{
    // Parameters
    [SerializeField]
    private float Begining_Angle = 0;
    [SerializeField]
    private float Rotation_Seed = 0;
    [SerializeField]
    private float Amplitude = 0;
    [SerializeField]
    private float Attack_Lenght = 0;
    [SerializeField]
    private float Attack_Delay = 0;
    [SerializeField]
    private GameObject Rotator;
    [SerializeField]
    private BoxCollider2D Lazer;
    [SerializeField]
    private Animator animator;
    // Variables
    private bool Is_Attacking = false;
    private bool Is_Enabled = false;


    // Update is called once per frame
    void Update()
    {
        // Make a repeating wave patern for the attack
        float Cos_Angle = Mathf.Sin(Time.time * Rotation_Seed) * Amplitude + Begining_Angle;
        // Rotate the lazer based on the pattern
        Rotator.transform.eulerAngles = new Vector3(0, 0, Cos_Angle);
    }
    public void Attack()
    {
        if (Is_Enabled)
        {
            // Start attack sequence
            StartCoroutine(Attack_Sequence());
        }
        
    }
    private IEnumerator Attack_Sequence()
    {
        yield return new WaitForSeconds(Attack_Delay);
        if (!Is_Attacking)
        {
            // Trigger animation
            animator.SetTrigger("Enabled");
            yield return new WaitForSeconds(4f/6f);
            Is_Attacking = true;
            Lazer.enabled = true;
            
        }
        yield return new WaitForSeconds(Attack_Lenght);
        // Trigger animation
        animator.SetTrigger("Disabled");
        Is_Attacking = false;
        Lazer.enabled = false;
        Attack();
    }
    public void Enable()
    {
        Is_Enabled = true;
        Attack();
    }
    public void Disable()
    {
        Is_Enabled = false;
    }
    
}
