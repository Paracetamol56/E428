using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_System : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private int Cinematic_ID_Overide = -1;
    [SerializeField]
    private int Animation_Cinematic_Begin_Trigger_Delay = 0;
    [SerializeField]
    private int Animation_Cinematic_End_Trigger_Delay = 1;
    // Variables
    private Animator PNJ_An;
    private Cinematic_Action Cinematic_Ac;
    private Dialog_System Dialog_Sy;
    // Start is called before the first frame update
    void Start()
    {
        // Get animator component
        PNJ_An = GetComponent<Animator>();
        // Get cinematic Action 
        Cinematic_Ac = GetComponentInChildren<Cinematic_Action>();
        // Change the Cinematic id to the override
        Cinematic_Ac.Update_Cinematic_Id(Cinematic_ID_Overide);
        // Get dialog sysytem
        Dialog_Sy = GetComponent<Dialog_System>();
        // Change the Cinematic id to the override
        Dialog_Sy.Update_Cinematic_Id(Cinematic_ID_Overide);
        Event_System.current.onCinematicBegin += Begin_Cinematic;
    }

    private void Begin_Cinematic(int id)
    {
        if (id == Cinematic_ID_Overide)
        {
            StartCoroutine(Cinematic());
        }
    }
    private IEnumerator Cinematic()
    {
        yield return new WaitForSeconds(Animation_Cinematic_Begin_Trigger_Delay);
        PNJ_An.SetTrigger("Cinematic_Begin");
        yield return new WaitForSeconds(Animation_Cinematic_End_Trigger_Delay);
        PNJ_An.SetTrigger("Cinematic_End");
    }
}
