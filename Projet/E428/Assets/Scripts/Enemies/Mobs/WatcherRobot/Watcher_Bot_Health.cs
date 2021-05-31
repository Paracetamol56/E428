using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher_Bot_Health : MonoBehaviour, IAttackable
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
    private WatcherRobot_States Watcher_Robot_St;
    private int Current_Health;
    private Watcher_Robot_Animation Watcher_Robot_An;
    // Start is called before the first frame update
    void Start()
    {
        Watcher_Robot_An = GetComponent<Watcher_Robot_Animation>();
        Watcher_Robot_St = GetComponent<WatcherRobot_States>();
        WatcherRobot_Mo = GetComponent<Mob_Basic_Movement>();
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
            Watcher_Robot_St.Update_State(WatcherRobot_States.Watcher_States.Agro);
            Watcher_Robot_An.Launch_Stunt_Animation();
        }
        else
        {
            // If bot is Dead
            // Disable Attack Box
            Watcher_Robot_St.Update_State(WatcherRobot_States.Watcher_States.Dead);
            // Launch Animation
            Watcher_Robot_St.Be_Attacked();
            StartCoroutine(Destruction());
            Watcher_Robot_An.Launch_Death_Animation();
        }
    }
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
