using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Ground : MonoBehaviour
{
    private Doom_Shroom_Movement Doom_Shroom_Mo;
    private Doom_Shroom_Attack Doom_Shroom_At;
    private void Start()
    {
        Doom_Shroom_Mo = GetComponentInParent<Doom_Shroom_Movement>();
        Doom_Shroom_At = GetComponentInParent<Doom_Shroom_Attack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("IM ON THE GROUND");
        Doom_Shroom_Mo.Set_Grounded(true);
        Doom_Shroom_At.Launch_Attack();
    }
}
