using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Robor_Fire_Pivot : MonoBehaviour
{
    // VAriables
    private Rigidbody2D Fire_Robot_RB;
    // Start is called before the first frame update
    void Start()
    {
        Fire_Robot_RB = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the robot is going to the right
        if (Fire_Robot_RB.velocity.x > 0)
        {
            // rotate the fire pivot to have the spawner in the right place
            transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        }
        else
        {
            // rotate the fire pivot to have the spawner in the right place
            transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
        }
    }
}
