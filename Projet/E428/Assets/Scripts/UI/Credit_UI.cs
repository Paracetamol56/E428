using System.Collections;
using UnityEngine;
public class Credit_UI : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Delay_Before_Main_Menu = 35f;
    [SerializeField]
    private Level_Transition_Manager Level_Transition_Ma;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Main_Menu_Sequence());
    }
    private IEnumerator Main_Menu_Sequence()
    {
        // Wait for the middle of the animation then set last build index to 0 to disable continue option in main menu (^_^)
        yield return new WaitForSeconds(Delay_Before_Main_Menu / 2);
        Save_System.Save_Data(0);
        // Wait for the end of the animation then make a transition to main menu;
        yield return new WaitForSeconds(Delay_Before_Main_Menu / 2);
        Level_Transition_Ma.Load_A_Level(0);
    }
   
}
