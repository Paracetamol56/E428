using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB_Slime_Animation : MonoBehaviour
{
    [SerializeField]
    private int Cinematic_Id = 0;
    private Rigidbody2D RGB_Slime_RB;
    private SpriteRenderer RGB_Slime_SR;
    private Animator RGB_Slime_An;
    // Crash if not public for some reason
    private Audio_Prefab_Spawner Audio_Prefab_Sp;
    public RGB_Slime_States RGB_Slime_St;
    private float Horizontal_Velocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        RGB_Slime_RB = GetComponent<Rigidbody2D>();
        RGB_Slime_SR = GetComponent<SpriteRenderer>();
        RGB_Slime_An = GetComponent<Animator>();
        Audio_Prefab_Sp = GetComponent<Audio_Prefab_Spawner>();
        //RGB_Slime_St = GetComponent<RGB_Slime_States>(); // Dont work for some reason
        // Subscribe to cinematic event
        Event_System.current.onCinematicBegin += Launch_Cinematic;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal_Velocity = RGB_Slime_RB.velocity.x;
        RGB_Slime_An.SetFloat("Velocity_X", Horizontal_Velocity);
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Color());
    }
    IEnumerator Stunt_Color()
    {
        print("Stunt");
        RGB_Slime_SR.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        RGB_Slime_SR.color = Color.white;
    }
    public void Launch_Death_Animation()
    {
        RGB_Slime_An.SetBool("Is_Dead",true);
        Audio_Prefab_Sp.Play_A_Sound(1);
        Audio_Prefab_Sp.Play_A_Sound(2);
    }
    public void Launch_Attack_Animation()
    {
        RGB_Slime_An.SetTrigger("Attack");

    }
    public void Launch_Cinematic(int id)
    {
        if (id == Cinematic_Id)
        {
            StartCoroutine(Cinematic());
        }
        
    }
    private IEnumerator Cinematic()
    {
        // Check if the combat hasn't already began
        if (RGB_Slime_St.Get_States() == Boss_States.Cinematic)
        {
            // Wait for dialog end
            yield return new WaitForSeconds(10f);
            RGB_Slime_An.SetTrigger("Cinematic");
            Audio_Prefab_Sp.Play_A_Sound(3);
            //Debug.Log(" Before cinematic" + RGB_Slime_St.Get_States());
            yield return new WaitForSeconds(5f);
            RGB_Slime_St.Update_State(Boss_States.Attack);
            Debug.Log(" After cinematic" + RGB_Slime_St.Get_States());
            // Launch Boss Music and fade the mixers
            Audio_Mixer_Control.current.Fade_Music(-80, 0.6f);
            Audio_Mixer_Control.current.Fade_Boss(-0, 0.6f);
            GetComponent<AudioSource>().Play();
        }

    }
}
