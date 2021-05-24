using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Slime_Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            IAttackable attackable = collision.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.Be_Attacked();
            }
        }

    }
    public void Destroy_Self()
    {
        Destroy(gameObject);
    }
}
