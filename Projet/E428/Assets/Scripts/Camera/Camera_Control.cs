using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Camera_Control : MonoBehaviour
{
    private float Horizontal_Axis;
    [SerializeField]
    private float Horizontal_Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Horizontal_Axis * Time.deltaTime, 0, 0);
    }
    public void GetHorizontalAxis(InputAction.CallbackContext context)
    {
        Horizontal_Axis = context.ReadValue<float>() * Horizontal_Speed;
    }
}
