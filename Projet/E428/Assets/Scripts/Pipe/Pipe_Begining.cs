using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Begining : MonoBehaviour
{
    //Parameters
    [SerializeField]
    private Camera_Control Camera;
    [SerializeField]
    private float Camera_Damping = 5;
    [SerializeField]
    private List<GameObject> Pipe_Section;
    // Private variable
    private bool Is_Pipe_Enabled = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // The object that triggered the colision is a player
        if (collision.tag == "Player")
        {
            // Start the pipe animation
            if (Is_Pipe_Enabled)
            {
                
                StartCoroutine(Camera_Animation(collision));
            }
        }
    }
    // Pipe animation
    IEnumerator Camera_Animation(Collider2D collision)
    {
        // Move away the player
        collision.transform.position = new Vector3(1000, 1000, 1000);
        // Tell listeners that player has entered a pipe
        Event_System.current.Pipe_Entered();
        // Disable new animations
        Is_Pipe_Enabled = false;
        // Get state script from player
        Glucose_States state = collision.GetComponent<Glucose_States>();
        // If the script exist
        if (state != null)
        {
            // Change the control of the player to pipe
            Debug.Log("Updated Glucose control to Pipe");
            state.Change_Glucose_Controls(Glucose_States.Player_Control.Pipe);
        }


        // For all pipe sections
        for (int index = 0; index < Pipe_Section.Count; index++)
        {
            // Set the camer target to the current section
            Camera.Set_New_Target(Pipe_Section[index], Camera_Damping, false);
            // Then wait for half a second
            yield return new WaitForSeconds(0.5f);
        }
        // After that teleport the player to the last section and make the controls normal

        if (state != null)
        {
            state.Change_Glucose_Controls(Glucose_States.Player_Control.Normal);
        }
        // Freeze the player
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        // TP the player to tre last pipe
        collision.transform.position = Pipe_Section[Pipe_Section.Count - 1].transform.position;
        
        // Then reset camera target to the player
        Camera.Set_New_Target(collision.gameObject, 1, true);
        
        // Enable new aniamtions;
        Is_Pipe_Enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.25f);
        
    }
}
