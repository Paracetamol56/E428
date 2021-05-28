using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_System : MonoBehaviour
{
    [SerializeField,TextArea(3,3)]
    private List<string> Dialog;
    [SerializeField]
    private int Cinematic_Id;

    // Start is called before the first frame update
    void Start()
    {
        Event_System.current.onCinematicBegin += Launch_Dialog;
    }
    public void Update_Cinematic_Id(int id)
    {
        Cinematic_Id = id;
    }

    public void Launch_Dialog(int id)
    {
        if (id == Cinematic_Id)
        {
            Debug.Log("Transfer dialog to global");
            Global_Variable.Global_Dialog = Dialog;
            Event_System.current.Dialog_Started();
        }
    }
}
