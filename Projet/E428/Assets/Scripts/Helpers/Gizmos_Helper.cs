using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmos_Helper : MonoBehaviour
{
    [SerializeField]
    private Color GizmoColor = Color.white;
    [SerializeField]
    private Vector3 Gizmos_Size = new Vector3(.5f,.5f,.5f);
    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, Gizmos_Size);
    }
}
