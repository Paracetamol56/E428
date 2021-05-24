using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric_Robot_Lighting_Bolt : MonoBehaviour
{

    // Parameters
    [SerializeField]
    private LayerMask GroundMask;
    [SerializeField]
    private GameObject Bolt;
    [SerializeField]
    private float Time_Before_Attack = 0.5f;
    [SerializeField]
    private float Time_After_Attack = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        // Teleport the Origin to the ground
        RaycastHit2D Raycast_Info = Physics2D.Raycast(transform.position, -transform.up, 50, GroundMask);
        if (Raycast_Info.collider != null)
        {
            transform.position = Raycast_Info.point;
        }
        StartCoroutine(Enable_Attack_Trigger());
    }

    private IEnumerator Enable_Attack_Trigger()
    {
        yield return new WaitForSeconds(Time_Before_Attack);
        Bolt.SetActive(true);
        yield return new WaitForSeconds(Time_After_Attack);
        Destroy(gameObject);
    }
}
