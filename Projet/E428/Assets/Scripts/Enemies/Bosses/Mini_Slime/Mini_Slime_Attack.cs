using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Slime_Attack : MonoBehaviour
{
    // Parameters
    
    // Variable
    private bool Is_Enabled = true;
    private Mini_Slime_Animation Mini_Slime_An;
    private BoxCollider2D Attack_Collider;
    private bool Dead = false;
    private Mini_Slime_Movement Mini_Slime_Mo;
    private Mini_Slime_Heath Mini_Slime_He;
    // Start is called before the first frame update
    void Start()
    {
        Mini_Slime_An = GetComponentInParent<Mini_Slime_Animation>();
        Attack_Collider = GetComponent<BoxCollider2D>();
        Mini_Slime_Mo = GetComponentInParent<Mini_Slime_Movement>();
        Mini_Slime_He = GetComponentInParent<Mini_Slime_Heath>();
    }
    // Call Attack Avtion when the player is in contact
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the game object can be attacked
        IAttackable attackable = collision.GetComponent<IAttackable>();
        // Check if the object is the player
        if (attackable != null)
        {
            if (collision.tag == "Player")
            {
                // If the attack is enabled
                if (Is_Enabled)
                {
                    StartCoroutine(Attack_Sequence(attackable));
                }
            }
            else if (Dead)
            {
                StartCoroutine(Attack_Sequence(attackable));
            }
        }
        
    }
    public void Launch_Death_Attack()
    {
        Dead = true;
        // Reanable the trigger to call the onTrigger event
        Attack_Collider.enabled = false;
        Attack_Collider.enabled = true;
        // Call coroutine in case attack trigger found nothing with null argument to not attack at all;
        StartCoroutine(Attack_Sequence(null));
    }
    private IEnumerator Attack_Sequence(IAttackable attackable)
    {
        // Stop other coroutine exectution
        Is_Enabled = false;
        // Check used only to prevent crash when coroutine called frome Launch death animation
        if (attackable != null)
        {
            attackable.Be_Attacked();
        }
        // Launch animation 
        Mini_Slime_An.Launch_Death_Animation();
        // Stop the mob from moving
        Mini_Slime_Mo.Set_Life(false);
        // Wait for the animation to finish
        yield return new WaitForSeconds(0.5f);
        // Destroy entire game object by calling the finction in a parent script
        Mini_Slime_He.Destroy();
    }
    
}
