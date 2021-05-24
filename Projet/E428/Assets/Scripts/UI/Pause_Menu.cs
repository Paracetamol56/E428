using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject Pause_UI;
    [SerializeField]
    private Level_Transition_Manager Transition_Manager;


    private void Start()
    {
        Event_System.current.onPauseGame += Switch_Pause;
    }
    private void OnDestroy()
    {
        Event_System.current.onPauseGame -= Switch_Pause;
    }
    public void Switch_Pause()
    {
        // Used to switch pause mod via pause key
        if (Global_Variable.Current_State == Game_State.Pause)
        {
            Unpause_Game();
        }
        else
        {
            Pause_Game();
        }
    }
    public void Pause_Game()
    {
        Debug.Log("Pause UI Pause");
        if (Global_Variable.Current_State == Game_State.Player_Control)
        {
            // Set paused timescale
            Time.timeScale = 0f;
            // Set global pause
            GameManager.Update_Game_State(Game_State.Pause);
            Pause_UI.SetActive(true);
        }
        
    }
    public void Unpause_Game()
    {
        // Set normal timescale
        Time.timeScale = 1f;
        // Unset global pause
        GameManager.Update_Game_State(Game_State.Player_Control);
        Pause_UI.SetActive(false);
    }
    public void Return_To_Main_Menu()
    {
        // Unset global pause
        GameManager.Update_Game_State(Game_State.Player_Control);

        // Set normal timescale
        Time.timeScale = 1f;
        // Load main menu scene
        Transition_Manager.Load_A_Level(0);

    }
    public void Quit_Game()
    {

        Debug.Log("Quit App");
        Application.Quit();
    }
    
}
