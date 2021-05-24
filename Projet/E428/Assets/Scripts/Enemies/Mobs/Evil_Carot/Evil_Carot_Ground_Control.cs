using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil_Carot_Ground_Control : MonoBehaviour
{
    [SerializeField]
    private Evil_Carot_Movement Evil_Carot_Mo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ground Control Trigger");
        Evil_Carot_Mo.Ground_Control();
    }
}
