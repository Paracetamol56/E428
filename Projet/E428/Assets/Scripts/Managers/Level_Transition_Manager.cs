using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Transition_Manager : MonoBehaviour
{
    [SerializeField]
    private Animator Transition_AC;
    [SerializeField]
    private float Transition_Time = 1f;

    private void Start()
    {
        // Set Player Global Progression
        
        Global_Variable.Last_Level_Build_Index = SceneManager.GetActiveScene().buildIndex;
        Save_System.Save_Data(SceneManager.GetActiveScene().buildIndex);
        Event_System.current.onReloadLevel += Reload_Level;
    }
    

    public void Load_Next_Level()
    {
        // Start Transition
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Used when palyer die
    public void Reload_Level()
    {
        // Start Transition
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void Load_A_Level(int Index)
    {
        // Load the level with the same build index as the parametter
        StartCoroutine(LoadLevel(Index));
    }

    // Level transition coroutine
    IEnumerator LoadLevel(int Level_Build_Index)
    {
        Transition_AC.SetTrigger("Start_Transition");
        yield return new WaitForSeconds(Transition_Time);
        SceneManager.LoadScene(Level_Build_Index);
    }
}
