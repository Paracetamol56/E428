using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject BTN_Continue;

    private int Overridden_Difficulty = -1;
   
    
    private void Awake()
    {
        Save_System.Recover_Data();
        BTN_Continue.SetActive(Global_Variable.Last_Level_Build_Index != 0);
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
}
