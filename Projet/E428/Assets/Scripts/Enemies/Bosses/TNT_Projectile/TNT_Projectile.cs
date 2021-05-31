using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Projectile : MonoBehaviour
{
    // Parametter
    [SerializeField]
    private float Default_Speed = 5;
    // Variables
    private Rigidbody2D TNT_RB;
    private Animator TNT_An;
    // Start is called before the first frame update
    void Start()
    {
        TNT_RB = GetComponent<Rigidbody2D>();
        TNT_An = GetComponent<Animator>();
        TNT_RB.velocity = transform.up * Default_Speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Explosion " + Time.time);
        IAttackable enemy = collision.GetComponent<IAttackable>();
        if (enemy != null)
        {
            enemy.Be_Attacked();
        }
        TNT_An.SetTrigger("Explode");
        // Stop the TNT
        TNT_RB.velocity = new Vector2(0, 0);

        
        StartCoroutine(Explode());
    }
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
