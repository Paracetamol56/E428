using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose_Splotch_Sound : MonoBehaviour
{
    private Audio_Prefab_Spawner Audio_Prefab_Sp;
    // Start is called before the first frame update
    void Start()
    {
        Audio_Prefab_Sp = GetComponentInParent<Audio_Prefab_Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Audio_Prefab_Sp.Play_A_Sound(2);
    }
}
