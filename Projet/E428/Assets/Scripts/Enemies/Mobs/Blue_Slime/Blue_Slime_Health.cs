using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Slime_Health : MonoBehaviour, IAttackable
{
    // Parametters
    [SerializeField]
    private int Health_Easy = 5;
    [SerializeField]
    private int Health_Normal = 6;
    [SerializeField]
    private int Health_Hard = 7;
    // Variables
    private Mob_Basic_Movement WatcherRobot_Mo;
    private int Current_Health;
    private Blue_Slime_Animation Blue_Slime_An;
    private Blue_Slime_Explode Blue_Slime_Ex;
    private Audio_Prefab_Spawner Audio_Prefab_Sp;
    // Start is called before the first frame update
    void Start()
    {
        Blue_Slime_Ex = GetComponentInChildren<Blue_Slime_Explode>();
        Blue_Slime_An = GetComponent<Blue_Slime_Animation>();
        WatcherRobot_Mo = GetComponent<Mob_Basic_Movement>();
        Audio_Prefab_Sp = GetComponent<Audio_Prefab_Spawner>();
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
        WatcherRobot_Mo.Set_Agro(true);
        Current_Health--;
        print("Watcher Agro");
        if (Current_Health > 0)
        {
            // If bot is alive
            // Make mob angry

            Blue_Slime_An.Launch_Stunt_Animation();
        }
        else
        {
            // If bot is Dead
            // Disable Attack Box

            // Launch Animation
            Audio_Prefab_Sp.Play_A_Sound(0);
            StartCoroutine(Destruction());
            Blue_Slime_An.Launch_Death_Animation();
            
        }
    }
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(0.5f);
        Blue_Slime_Ex.Explode();

    }
}

