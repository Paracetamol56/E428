using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity_Manager_Animation : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private int Cinematic_Id = 0;
    [SerializeField]
    private float Cast_Lenght = 0.5f;
    // Variables
    private SpriteRenderer Electricity_Manager_Sr;
    private Animator Electricity_Manager_An;
    private Electricity_Manager_State Electricity_Manager_St;
    private Boss_States State = Boss_States.Cinematic;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        Electricity_Manager_Sr = GetComponent<SpriteRenderer>();
        Electricity_Manager_An = GetComponent<Animator>();
        Electricity_Manager_St = GetComponent<Electricity_Manager_State>();
        Event_System.current.onCinematicEnd += Launch_End_Cinematic;
        Event_System.current.onCinematicBegin += Launch_Cinematic;
    }

    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Color());
    }
    IEnumerator Stunt_Color()
    {
        print("Stunt");
        Electricity_Manager_Sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Electricity_Manager_Sr.color = Color.white;
    }
    private void Launch_Cinematic(int id)
    {
        if (Cinematic_Id == id)
        {
            // Launch Boss Music and fade the mixers
            Audio_Mixer_Control.current.Fade_Music(-80, 0.6f);
            Audio_Mixer_Control.current.Fade_Boss(-0, 0.6f);
        }

    }

    public void Launch_Death_Animation()
    {
        Debug.Log("Doom Shroom Death animation");
        Electricity_Manager_An.SetTrigger("Dead");
    }
    public void Launch_Cast_Animation()
    {
        StartCoroutine(Cast_Animation());
    }
    private IEnumerator Cast_Animation()
    {
        Electricity_Manager_An.SetTrigger("Cast_Start");
        yield return new WaitForSeconds(Cast_Lenght);
        Electricity_Manager_An.SetTrigger("Cast_End");
    }
    private void Launch_End_Cinematic(int id)
    {
        if (Cinematic_Id == id)
        {
            // Launch end cinematic animation
            Electricity_Manager_An.SetTrigger("Cinematic_End");
            if (State == Boss_States.Cinematic)
            {
                Electricity_Manager_St.Update_State(Boss_States.Attack);
            }
        }
    }
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Electricity Manager An state = " + State);
    }
}

