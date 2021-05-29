using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Robo_Fire_Spray : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Time_Before_Enabled = 0.5f;
    [SerializeField]
    private float Time_Before_Disabled = 1.5f;
    [SerializeField]
    private float Time_Before_Destroyed= 2f;
    [SerializeField]
    private GameObject Parent;
    // Variable
    private bool Is_Enabled = true;
    private BoxCollider2D Fire_HitBox;
    // Start is called before the first frame update
    void Start()
    {
        Fire_HitBox = GetComponent<BoxCollider2D>();
        StartCoroutine(Spray_Hit_Toggle());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Is_Enabled)
        {
            IAttackable attackable = collision.GetComponent<IAttackable>();
            if (attackable != null && collision.tag == "Player")
            {
                attackable.Be_Attacked();
            }
        }

    }
    private IEnumerator Spray_Hit_Toggle()
    {
        // Wait for the fire to form
        yield return new WaitForSeconds(Time_Before_Enabled);
        // Enable attack
        Fire_HitBox.enabled = true;
        // Wait for the fire to disipate
        yield return new WaitForSeconds(Time_Before_Disabled - Time_Before_Enabled);
        // Disable attack
        Fire_HitBox.enabled = false;
        // Wait before destruction 
        yield return new WaitForSeconds(Time_Before_Destroyed - Time_Before_Disabled - Time_Before_Enabled);
        // Destroy the parent game object
        Destroy(Parent);
    }
}
