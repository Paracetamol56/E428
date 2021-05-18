using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Prefered_Level_Trigger : MonoBehaviour
{
    [SerializeField]
    private Level_Transition_Manager Manager;
    [SerializeField]
    private int Level_Build_Index;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Manager.Load_A_Level(Level_Build_Index);
        }
    }
}
