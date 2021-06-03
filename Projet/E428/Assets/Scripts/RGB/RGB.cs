using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private int Cinematic_Id = 0;
    // Variables 
    private Animator RGB_An;

    // Start is called before the first frame update
    void Start()
    {
        RGB_An = GetComponent<Animator>();
        Event_System.current.onCinematicEnd += Unicorn_Puke_Launch;
    }
    // End of dev so f#ck it for the name but it's still a descriptive name
    private void Unicorn_Puke_Launch(int UwU_Id)
    {
        if (Cinematic_Id == UwU_Id)
        {
            RGB_An.SetTrigger("RGB_Power_ENGAGED");
        }
    }
}
