using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mob_Basic_Attack : MonoBehaviour
{
    [HideInInspector]
    public bool Is_Enabled = true;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Is_Enabled)
        {
            IAttackable attackable = collision.GetComponent<IAttackable>();
            if (attackable != null && collision.tag == "Player")
            {
                attackable.Be_Attacked();
            }
        }
        
    }
}
