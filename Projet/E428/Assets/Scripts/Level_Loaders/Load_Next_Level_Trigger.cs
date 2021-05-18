using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Next_Level_Trigger : MonoBehaviour
{
    [SerializeField]
    private Level_Transition_Manager Manager;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Manager.Load_Next_Level();
        }
    }
}
