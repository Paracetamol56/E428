using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Slime_Explode : MonoBehaviour
{

    // Parametters
    [SerializeField]
    private float Explosion_Delay_Hard = 0.25f;
    [SerializeField]
    private float Explosion_Delay_Normal = 0.5f;
    [SerializeField]
    private float Explosion_Delay_Easy = 0.75f;
    [SerializeField]
    private GameObject Explostion_Prefab;
    [SerializeField]
    private GameObject Parent;
    // Variables
    private float Current_Explosion_Delay;
    private Blue_Slime_Animation Blue_Slime_An;
    // Start is called before the first frame update
    void Start()
    {
        Blue_Slime_An = GetComponentInParent<Blue_Slime_Animation>();
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Explosion_Delay = Explosion_Delay_Hard;
                break;
            case 1:
                Current_Explosion_Delay = Explosion_Delay_Normal;
                break;
            default:
                Current_Explosion_Delay = Explosion_Delay_Easy;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // The object that triggered the colision is a player
        if (collision.tag == "Player")
        {
            Explode();
        }
    }
    public void Explode()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        Blue_Slime_An.Launch_Death_Animation();
        yield return new WaitForSeconds(Current_Explosion_Delay);
        Instantiate(Explostion_Prefab, transform.position + new Vector3(0,0.5f,0), transform.rotation );
        Destroy(Parent);
    }
}
