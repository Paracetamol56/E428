using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Next_Level_Trigger : MonoBehaviour
{
    [SerializeField]
    private Level_Transition_Manager Manager;
    [SerializeField]
    private float Transition_Delay = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player is in the right spot
        if (collision.tag == "Player")
        {
            // Load the level with the next build index
            StartCoroutine(Transition());
        }
    }
    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(Transition_Delay);
        Manager.Load_Next_Level();
    }

}
