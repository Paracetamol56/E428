using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil_Carot_Health : MonoBehaviour, IAttackable
{
    // Parametters
    [SerializeField]
    private int Health_Easy = 2;
    [SerializeField]
    private int Health_Normal = 3;
    [SerializeField]
    private int Health_Hard = 5;
    // Variables
    private Evil_Carot_Movement evil_Carot_Mo;
    private int Current_Health;
    private Evil_Carot_Animation Evil_Carot_An;


    // Start is called before the first frame update
    void Start()
    {

        Evil_Carot_An = GetComponent<Evil_Carot_Animation>();
        evil_Carot_Mo = GetComponent<Evil_Carot_Movement>();
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Health = Health_Hard;
                break;
            case 1:
                Current_Health = Health_Normal;
                break;
            default:
                Current_Health = Health_Easy;
                break;
        }
    }
    public void Be_Attacked()
    {
        evil_Carot_Mo.Set_Agro(true);
        Current_Health--;
        print("Watcher Agro");
        if (Current_Health > 0)
        {
            // If bot is alive
            // Make mob angry

            Evil_Carot_An.Launch_Stunt_Animation();
        }
        else
        {
            // If bot is Dead
            // Disable Attack Box

            // Launch Animation

            StartCoroutine(Destruction());
            Evil_Carot_An.Launch_Death_Animation();

        }
    }
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
