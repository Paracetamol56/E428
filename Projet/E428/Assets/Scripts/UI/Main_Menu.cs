using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Main_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject BTN_Continue;

    private int Overridden_Difficulty = -1;

    [SerializeField]
    private AudioMixer Audio_Mixer;
    [SerializeField]
    private Slider Master_Slider;
    [SerializeField]
    private Slider Music_Slider;
    [SerializeField]
    private Slider FX_Slider;

    private void Awake()
    {
        Save_System.Recover_Data();
    }

    private void Start()
    {
        if (Global_Variable.Last_Level_Build_Index <= 0)
        {
            Wipe_Button();
        }
    }
    // Launch new playthrough
    public void New_Game()
    {
        Global_Variable.Last_Level_Build_Index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    // Launch previous playthrough
    public void Continue_Game()
    {

        // If the difficulty has been overridden by the player set the global difficulty to that value;
        if (Overridden_Difficulty > -1)
            Global_Variable.Difficulty_Level = Overridden_Difficulty;
        // Load lastest played scene;
        SceneManager.LoadScene(Global_Variable.Last_Level_Build_Index);
    }

    public void Update_Overriddent_Difficulty(int Diff)
    {
        // Prevent illegal value
        switch (Diff)
        {
            case 2:
                Overridden_Difficulty = 2;
                break;
            case 1:
                Overridden_Difficulty = 1;
                break;
            default:
                Overridden_Difficulty = 0;
                break;
        }
    }

    public void Wipe_Button()
    {
        Save_System.Wipe_Data();
        BTN_Continue.SetActive(false);
    }

    public void Quit_Button()
    {
        Debug.Log("Quit App");
        Application.Quit();
    }

    public void OnMasterSliderValueChanged()
    {
        Audio_Mixer.SetFloat("Master", Mathf.Log10(Master_Slider.value) * 20);
    }

    public void OnMusicSliderValueChanged()
    {
        Audio_Mixer.SetFloat("Music", Mathf.Log10(Music_Slider.value) * 20);
    }

    public void OnFXSliderValueChanged()
    {
        Audio_Mixer.SetFloat("FX", Mathf.Log10(FX_Slider.value) * 20);
    }
}
