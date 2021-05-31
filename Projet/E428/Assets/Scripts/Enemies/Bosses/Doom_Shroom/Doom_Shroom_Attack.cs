using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Attack : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private GameObject Spore;
    [SerializeField]
    private Transform Spore_Spawn_Point;
    // Variables
    private Boss_States State = Boss_States.Attack;
    private GameObject Instancied_Spore;
    // Start is called before the first frame update

    public void Launch_Attack()
    {
        // If the boss is in attack mode
        if (State == Boss_States.Attack)
        {
            Instancied_Spore = Instantiate(Spore, Spore_Spawn_Point);
            Instancied_Spore.transform.parent = null;
        }
        

    }
    // Update state when called
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Mouvement States = " + State);
    }
}
