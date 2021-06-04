using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Load_Next_Level : MonoBehaviour
{
    [SerializeField]
    private Level_Transition_Manager Manager;

    // Load next level (yes)
    public void Load_Next_Level()
    {
        StartCoroutine(Bye_Bye_Boss());
    }
    private IEnumerator Bye_Bye_Boss()
    {
        // Wait for a bit to be happy
        yield return new WaitForSeconds(6);
        Manager.Load_Next_Level();
    }
}
