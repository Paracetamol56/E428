using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Spore : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Spore_Duration = 3f;
    [SerializeField]
    private float Dissipation_Duration = 0.5f;
    // Variables
    private Animator Spore_An;
    // Start is called before the first frame update
    void Start()
    {
        Spore_An = GetComponent<Animator>();
        StartCoroutine(Spore_Disipate());
    }

    private IEnumerator Spore_Disipate()
    {
        // Wait for the time when the dissipation begin
        yield return new WaitForSeconds(Mathf.Clamp(Spore_Duration - Dissipation_Duration, 0, 10));
        Spore_An.SetTrigger("Dissipate");
        // Then wait for the dissipation to end
        yield return new WaitForSeconds(Dissipation_Duration);
        Destroy(gameObject);    

    }
}
