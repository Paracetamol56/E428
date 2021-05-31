using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doom_Shroom_Animation : MonoBehaviour
{
    // Parametter
    [SerializeField]
    private float Cinematic_Id = 0;
    // Variables
    private Animator Doom_Shroom_An;
    private Rigidbody2D Doom_Shroom_RB;
    private SpriteRenderer Doom_Shroom_SR;
    private Doom_Shroom_State Doom_Shroom_St;
    private bool Is_Facing_Right = false;
    private float Horizontal_Velocity = 0;
    private Audio_Prefab_Spawner Audio_Prefab_Sp;
    // Start is called before the first frame update
    void Start()
    {
        Audio_Prefab_Sp = GetComponent<Audio_Prefab_Spawner>();
        Doom_Shroom_An = GetComponent<Animator>();
        Doom_Shroom_RB = GetComponent<Rigidbody2D>();
        Doom_Shroom_SR = GetComponent<SpriteRenderer>();
        Doom_Shroom_St = GetComponent<Doom_Shroom_State>();
        Event_System.current.onCinematicEnd += Launch_Cinematic;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal_Velocity = Doom_Shroom_RB.velocity.x;

        if (Is_Facing_Right && Horizontal_Velocity < 0)
            Look_Left();
        else if (!Is_Facing_Right && Horizontal_Velocity > 0)
            Look_Right();
    }
    private void Look_Right()
    {
        Is_Facing_Right = true;
        Doom_Shroom_SR.flipX = true;
    }
    private void Look_Left()
    {
        Is_Facing_Right = false;
        Doom_Shroom_SR.flipX = false;
    }
    public void Launch_Jump_Animation()
    {
        Doom_Shroom_An.SetTrigger("Jump");
    }
    public void Launch_Death_Animation()
    {
        Debug.Log("Doom Shroom Death animation");
        Doom_Shroom_An.SetBool("Dead", true);
        Audio_Prefab_Sp.Play_A_Sound(0);
    }
    public void Launch_Attack_Animation()
    {
        Doom_Shroom_An.SetTrigger("Attack");
    }
    public void Launch_Cinematic(int id)
    {
        if (id == Cinematic_Id)
        {
            Doom_Shroom_St.Update_State(Boss_States.Attack);
            // Launch Boss_Music
            GetComponent<AudioSource>().Play();
            // Fade in boss music
            Audio_Mixer_Control.current.Fade_Boss(0, 1.0f);
            // Fade out normal music
            Audio_Mixer_Control.current.Fade_Music(-80,1f);
        }
    }
    public void Launch_Stunt_Animation()
    {
        StartCoroutine(Stunt_Color());
    }
    IEnumerator Stunt_Color()
    {
        print("Stunt");
        Doom_Shroom_SR.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Doom_Shroom_SR.color = Color.white;
    }
}
