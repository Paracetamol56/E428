using System.Collections;
using UnityEngine;

public class Mob_Basic_Health : MonoBehaviour,IAttackable
{
    // Parametters
    [SerializeField]
    private int Health_Easy = 5;
    [SerializeField]
    private int Health_Normal = 6;
    [SerializeField]
    private int Health_Hard = 7;
    // Variables
    private Mob_Basic_Movement Mob_Basic_Mo;
    private Mob_Basic_Animation Mob_Basic_An;
    private int Current_Health;

    // Start is called before the first frame update
    void Start()
    {
        Mob_Basic_Mo = GetComponent<Mob_Basic_Movement>();
        Mob_Basic_An = GetComponent<Mob_Basic_Animation>();
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
        Mob_Basic_Mo.Set_Agro(true);
        Current_Health--;
        
        if (Current_Health > 0)
        {
            // If bot is alive
            Mob_Basic_An.Launch_Stunt_Animation();
        }
        else
        {
            // If bot is Dead
            // Disable Attack Box
            GetComponentInChildren<Mob_Basic_Attack>().Is_Enabled = false;
            // Launch Animation
            Mob_Basic_An.Launch_Death_Animation();
            Mob_Basic_Mo.Set_Life(false);
            StartCoroutine(Destruction());
        }
    }
    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
