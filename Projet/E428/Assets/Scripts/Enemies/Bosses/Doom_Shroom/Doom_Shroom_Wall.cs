using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Wall : MonoBehaviour
{
    private Doom_Shroom_Movement Doom_Shroom_Mo;
    // Start is called before the first frame update
    void Start()
    {
        // Get the script
        Doom_Shroom_Mo = GetComponentInParent<Doom_Shroom_Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make doom shroom change direction
        Doom_Shroom_Mo.Change_Direction();
    }
}
