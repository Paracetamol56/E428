using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB_Slime_Attack : MonoBehaviour
{

    // Parameters
    [SerializeField]
    private float Easy_Wait = 5f;
    [SerializeField]
    private float Normal_Wait = 4f;
    [SerializeField]
    private float Hard_Wait = 3f;
    [SerializeField]
    private int Number_Of_Mini_Slimes = 0;
    [SerializeField]
    private float Mini_Slimes_Spawn_Delay = 1.5f;
    [SerializeField]
    private Transform Mini_Slime_Spawn_Point;
    [SerializeField]
    private GameObject Mini_Slime_Prefab;
    // Variables
    private Boss_States State = Boss_States.Cinematic;
    private RGB_Slime_Animation RGB_Slime_An;
    private bool Can_Attack = true;
    private RGB_Slime_States RGB_Slime_St;
    private float Current_Wait_Time;
    private Audio_Prefab_Spawner Audio_Prefab_Sp;
    // Start is called before the first frame update
    void Start()
    {
        RGB_Slime_An = GetComponent<RGB_Slime_Animation>();
        RGB_Slime_St = GetComponent<RGB_Slime_States>();
        Audio_Prefab_Sp = GetComponent<Audio_Prefab_Spawner>();

        // Switch RGB Slime Attack difficulty
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Wait_Time = Hard_Wait;
                break;
            case 1:
                Current_Wait_Time = Normal_Wait;
                break;
            default:
                Current_Wait_Time = Easy_Wait;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the boss is not in an attack salvo and is in Attack state
        if (Can_Attack && State == Boss_States.Attack)
        {
            // Launch the attack salvo
            StartCoroutine(Attack_Salvo());
        }
    }
    // Used to make the boss pause the attack after an attack salvo
    private IEnumerator Wait()
    {
            RGB_Slime_St.Update_State(Boss_States.Wait);
            yield return new WaitForSeconds(Current_Wait_Time);
            RGB_Slime_St.Update_State(Boss_States.Attack);
    }
    // Attack salvo logic
    private IEnumerator Attack_Salvo()
    {
        // If the boss is alive
        if (State != Boss_States.Dead)
        {
            // Block salvo superpositions
            Can_Attack = false;
            // Launch the action for the desired number of Mini Slimes
            for (int i = 0; i < Number_Of_Mini_Slimes; i++)
            {
                // Delay the spawn for gameplay purposes
                yield return new WaitForSeconds(Mini_Slimes_Spawn_Delay);
                // Launch attack animations if the boss is not dead 
                if (State != Boss_States.Dead)
                {
                    RGB_Slime_An.Launch_Attack_Animation();
                    // Launch attack logic
                    StartCoroutine(Attack_Action());
                }
            }
            // Lauch wait action
            StartCoroutine(Wait());
            // Reenable the call of the Coroutine
            Can_Attack = true;
        }

    }
    // Launching slime logic
    private IEnumerator Attack_Action()
    {
        // Wait for the good animation frame;
        yield return new WaitForSeconds(4f/6f); // Fourth frame of the animation
        Debug.Log("Attack Launched at " +Time.time); // TODO: Launch Logic
        if (State != Boss_States.Dead)
        {
            // Play sound
            Audio_Prefab_Sp.Play_A_Sound(0);
            // Spawn Mini_Slime
            Instantiate(Mini_Slime_Prefab, Mini_Slime_Spawn_Point);
        }
        
    }
    // Update states
    public void Update_State(Boss_States state)
    {
        State = state;
    }
    // Decrase the time between launches
    public void Increase_Difficulty()
    {
        print("Diff++");
        Number_Of_Mini_Slimes ++;
    }
}
