using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Glucose_Pause : MonoBehaviour
{
    public void Get_Pause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Game Input -> Pause");
            Event_System.current.Pause_Game();
        }
    }
}
