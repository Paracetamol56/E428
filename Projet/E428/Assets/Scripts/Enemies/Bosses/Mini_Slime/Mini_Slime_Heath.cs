using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Slime_Heath : MonoBehaviour,IAttackable
{

    // Variables
    private Mini_Slime_Attack Mini_Slime_Att;
    
    
    private void Start()
    {
        Mini_Slime_Att = GetComponentInChildren<Mini_Slime_Attack>();
        
    }
    public void Be_Attacked()
    {
        Mini_Slime_Att.Launch_Death_Attack();

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
