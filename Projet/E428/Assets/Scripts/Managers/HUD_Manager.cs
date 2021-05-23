using UnityEngine;
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
    // Variables
    private float Glucose_Health_Normalised = 1;
    private float Boss_Health_Normalised = 1;
    private bool Is_In_Battle = false;

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
        Is_In_Battle = true;
        Boss_Health_Animator.SetBool("Is_In_Battle", true);
    }
    // Stop to display Boss Health bar
    public void End_Battle()
    {
        Is_In_Battle = false;
        Boss_Health_Animator.SetBool("Is_In_Battle", false);
    }
}
