using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose_Spit : MonoBehaviour
{
    [SerializeField]
    private GameObject Impact;
    [SerializeField]
    private float Default_Speed = 5;

    private Rigidbody2D Glucose_RB;


    // constructor
    public Glucose_Spit(float Speed)
    {
        Default_Speed = Speed;  
    }

    // Start is called before the first frame update
    void Start()
    {
        Glucose_RB = GetComponent<Rigidbody2D>();
        Glucose_RB.velocity = -transform.right * Default_Speed + new Vector3(0, Default_Speed / 2, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAttackable enemy = collision.GetComponent<IAttackable>();
        if (enemy != null)
        {
            enemy.Be_Attacked();
        }
        //Instantiate(Impact, transform);
        Destroy(gameObject);
    }
}
