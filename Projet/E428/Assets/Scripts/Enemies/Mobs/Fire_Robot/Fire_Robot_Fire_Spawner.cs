using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Robot_Fire_Spawner : MonoBehaviour
{

    // Parametters
    [SerializeField]
    private float Time_Before_Attack = 3;
    [SerializeField]
    private GameObject Fire_Prefab;
    // Varaiable
    private float Next_Attack;
    // Start is called before the first frame update
    void Start()
    {
        Next_Attack = Time_Before_Attack;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > Next_Attack)
        {
            // Set next Attack minimum time
            Next_Attack = Time.time + Time_Before_Attack;
            // Instantiate the attack prefab
            Instantiate(Fire_Prefab, transform);
        }
    }
}
