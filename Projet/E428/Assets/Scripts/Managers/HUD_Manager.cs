using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private Slider Glucose_Health_Slider;
    [SerializeField]
    private float Glucose_Health_Slider_Smoothing = 5f;
    [SerializeField]
    private Slider Boss_Health_Slider;
    [SerializeField]
    private float Boss_Health_Slider_Smoothing = 5f;
    [SerializeField]
    private Animator Boss_Health_Animator;
    [SerializeField]
    private Animator Dialog_Box_An;
    [SerializeField]
    private Animator Dialog_Animator;
    [SerializeField]
    private Text Dialog_Text;
    [SerializeField]
    private float Dialog_Delay = 1f;
    // Variables
    private float Glucose_Health_Normalised = 1;
    private float Boss_Health_Normalised = 1;
    private List<string> Dialog_List;

    private void Start()
    {
        // Subscribe to dialog event
        Event_System.current.onDialogStarted += Display_Dialog;
    }
    private void OnDestroy()
    {
        // Unsubscribe from dialog event
        Event_System.current.onDialogStarted -= Display_Dialog;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Visualy the health bar
        Glucose_Health_Slider.value = Mathf.Lerp(Glucose_Health_Slider.value, Glucose_Health_Normalised, Glucose_Health_Slider_Smoothing * Time.deltaTime);
        Boss_Health_Slider.value = Mathf.Lerp(Boss_Health_Slider.value, Boss_Health_Normalised, Boss_Health_Slider_Smoothing * Time.deltaTime);
    }
    // Update Health value
    public void Update_Glucose_Health_Bar(int Current_Health, int Max_Health)
    {
        // Update health bar 
        float Current_Health_float = Current_Health;
        Glucose_Health_Normalised = Current_Health_float / Max_Health;
        print(Glucose_Health_Normalised);

    }

    public void Update_Boss_Health_Bar(int Current_Health, int Max_Health)
    {
        // Update health bar 
        float Current_Health_float = Current_Health;
        Boss_Health_Normalised = Current_Health_float / Max_Health;
        print(Glucose_Health_Normalised);
    }
    // Display Boss Health bar
    public void Start_Battle()
    {
        Boss_Health_Animator.SetBool("Is_In_Battle", true);
    }
    // Stop to display Boss Health bar
    public void End_Battle()
    {
        Boss_Health_Animator.SetBool("Is_In_Battle", false);
    }
    public void Display_Dialog()
    {
        Debug.Log("Displaying_Dialog");
        Dialog_List = Global_Variable.Global_Dialog;
        StartCoroutine(Dialog_Animation()); 
    }
    private IEnumerator Dialog_Animation()
    {
        // Launch Dialog box fade in
        Dialog_Box_An.SetTrigger("Visible");
        yield return new WaitForSeconds(1);
        for (int i = 0; i < Dialog_List.Count; i++)
        {
            // Change the UI text
            Dialog_Text.text = Dialog_List[i];
            // Launch Dialog fade in
            Dialog_Animator.SetTrigger("Visible");
            yield return new WaitForSeconds(1.5f);
            // Launch Dialog fade out
            Dialog_Animator.SetTrigger("Invisible");
            yield return new WaitForSeconds(0.25f);
        }
        // Wait for dialog to disapear
        yield return new WaitForSeconds(0.25f);
        // Launch Dialog box fade out
        Dialog_Box_An.SetTrigger("Invisible");
    }
}
