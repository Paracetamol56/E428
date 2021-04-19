using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Camera_Control : MonoBehaviour
{

    // Parameters

    [SerializeField]
    private GameObject Target; // Camera target
    public bool Is_Manual_Offset_Enabled = true;
    [SerializeField]
    private Vector2 Manual_Offset;
    [SerializeField]
    private float Follow_Smoothing = 5f;

    private float Follow_Damping = 1f;
    [SerializeField]
    private float Offset_Smoothing = 5f;

    // Privates Variables
    private bool Is_Target_Facing_Right = false;
    private float Horizontal_Offset;
    private Rigidbody2D Target_RB;

    // Start is called before the first frame update
    void Start()
    {
        Horizontal_Offset = Manual_Offset.x;
        Target_RB = Target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check the target direction to know what in direction should be the offset (if the target has no rigid body then there no calculation based on velocity)
        if (Target_RB != null)
        {
            if (Target_RB.velocity.x > 0)
                Is_Target_Facing_Right = true;
            if (Target_RB.velocity.x < 0)
                Is_Target_Facing_Right = false;
        }

        // Check if the manual offset is enabled
        if (Is_Manual_Offset_Enabled)
        {
            // Make horizontal offset adjustement with lerp to make it smooth
            if (Is_Target_Facing_Right)
                Horizontal_Offset = Mathf.Lerp(Horizontal_Offset, Manual_Offset.x, Offset_Smoothing * Time.deltaTime);
            else
                Horizontal_Offset = Mathf.Lerp(Horizontal_Offset, -Manual_Offset.x, Offset_Smoothing * Time.deltaTime);
        }
        else
        {
            Horizontal_Offset = 0;
        }
        // Set the target position of the camera in 3d (-1z to make sure to not clip while being in 3d audio reach)
        Vector3 Target_Camera_Position = Target.transform.position + new Vector3(Horizontal_Offset,0, -1f);
        
        // Disable manual offset if target has no rigid body or if the option is disabled
        if(Target_RB != null && Is_Manual_Offset_Enabled)
            Target_Camera_Position += Vector3.up * Manual_Offset.y;
        // Lerp the current position of the camera with its target smoothly
        transform.position = Vector3.Lerp(transform.position, Target_Camera_Position, (Follow_Smoothing / Follow_Damping) * Time.deltaTime);
    }
    // Set new target and get potential rigid body of the target
    public void Set_New_Target(GameObject Camera_Target, float Damping, bool Manual_Offset)
    {
        Target = Camera_Target;
        Target_RB = Camera_Target.GetComponent<Rigidbody2D>();
        Is_Manual_Offset_Enabled = Manual_Offset;
        Follow_Damping = Damping;
    }
}
