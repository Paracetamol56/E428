using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Audio_Mixer_Control : MonoBehaviour
{
    // Parameters
    [SerializeField]
    private AudioMixer Mixer;
    
    [SerializeField]
    private int Max_Music_DB = 0;
    [SerializeField]
    private float Music_Transition_Speed = 1;
    [SerializeField]
    private float Music_Transition_Lenght = 20;

    [SerializeField]
    private int Max_Boss_DB = 0;
    [SerializeField]
    private float Boss_Transition_Speed = 1;
    [SerializeField]
    private float Boss_Transition_Lenght = 20;


    private float Master_Transition_Speed = 1;
    [SerializeField]
    private float Master_Transition_Lenght = 20;

    // Variables
    private int Music_Fade = 0;
    private bool Is_Music_Fading = false;
    private float Current_Music_Volume = 0;
    
    private int Boss_Fade = 0;
    private bool Is_Boss_Fading = false;
    private float Current_Boss_Volume = 0;

    private int Master_Fade = 0;
    private bool Is_Master_Fading = false;
    private float Current_Master_Volume = 0;

    public static Audio_Mixer_Control current;
    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        Set_Master_Volume(0);
        Set_Music_Volume(-80);
        Set_Boss_Volume(-80);
        Fade_Music(Max_Music_DB, Music_Transition_Speed);
    }
    private void Update()
    {
        // Music gestion
        if (Is_Music_Fading)
        {
            //Debug.Log("Current_Music_Volume " + Current_Music_Volume);
            Current_Music_Volume = Mathf.Clamp (Mathf.Lerp(Current_Music_Volume, Music_Fade, Music_Transition_Speed * Time.deltaTime ), -80f, Max_Music_DB);
            Mixer.SetFloat("Music", Current_Music_Volume);
        }
        // Boss Gestion
        if (Is_Boss_Fading)
        {
            //Debug.Log("Current_Boss_Volume " + Current_Boss_Volume);
            Current_Boss_Volume = Mathf.Clamp(Mathf.Lerp(Current_Boss_Volume, Boss_Fade, Boss_Transition_Speed * Time.deltaTime), -80f, Max_Boss_DB);
            Mixer.SetFloat("Boss_Music", Current_Boss_Volume);
        }
        // Master Gestion
        if (Is_Master_Fading)
        {
            //Debug.Log("Current_Master_Volume " + Current_Master_Volume);
            Current_Master_Volume = Mathf.Clamp(Mathf.Lerp(Current_Master_Volume, Master_Fade, Master_Transition_Speed * Time.deltaTime), -80f, 0);
            Mixer.SetFloat("Master", Current_Master_Volume);
        }

    }
    public void Fade_Music(int value, float speed)
    {
        StartCoroutine(Fade_Music_Action(value,speed));
    }
    private IEnumerator Fade_Music_Action(int value, float speed)
    {
        Music_Transition_Speed = speed;
        Is_Music_Fading = true;
        Music_Fade = value;
        yield return new WaitForSeconds(Music_Transition_Lenght);
        Is_Music_Fading = false;
    }

    public void Fade_Boss(int value, float speed)
    {
        StartCoroutine(Fade_Boss_Action(value,speed));
    }
    private IEnumerator Fade_Boss_Action(int value, float speed)
    {
        Boss_Transition_Speed = speed;
        Is_Boss_Fading = true;
        Boss_Fade = value;
        yield return new WaitForSeconds(Boss_Transition_Lenght);
        Is_Boss_Fading = false;
    }
    public void Fade_Master(int value, float speed)
    {
        StartCoroutine(Fade_Master_Action(value, speed));
    }
    private IEnumerator Fade_Master_Action(int value, float speed)
    {
        Master_Transition_Speed = speed;
        Is_Master_Fading = true;
        Master_Fade = value;
        yield return new WaitForSeconds(Master_Transition_Lenght);
        Is_Master_Fading = false;
    }

    public void Set_Music_Volume(int value)
    {
        Mixer.SetFloat("Music", value);
        Mixer.GetFloat("Music", out Current_Music_Volume);
    }
    public void Set_Boss_Volume(int value)
    {
        Mixer.SetFloat("Boss_Music", value);
        Mixer.GetFloat("Boss_Music", out Current_Boss_Volume);
    }
    public void Set_Master_Volume(int value)
    {
        Mixer.SetFloat("Master", value);
    }
}
