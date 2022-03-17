using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic_Action : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private int Cinematic_Id = 0;
    [SerializeField]
    private Camera_Control Camera;
    [SerializeField]
    private GameObject New_Target;
    [SerializeField]
    private GameObject Player;
    // Variables
    private bool Is_Enabled = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player enter 
        if (collision.tag == "Player" && Is_Enabled)
        {
            Start_Cinematic();
        }

    }
    // update the cinematic id 
    public void Update_Cinematic_Id(int id)
    {
        Cinematic_Id = id;
    }
    // Launch cinematic
    public void Start_Cinematic()
    {
        Is_Enabled = false;
        // Set camera target to a new object
        Camera.Set_New_Target(New_Target, 3, false);
        // Launch cinematic event
        Event_System.current.Cinematic_Begin(Cinematic_Id);
    }
    public void End_Cinematic()
    {
        // Set camera target to the player
        Camera.Set_New_Target(Player, 1, true);
        Destroy(gameObject);
    }
}
