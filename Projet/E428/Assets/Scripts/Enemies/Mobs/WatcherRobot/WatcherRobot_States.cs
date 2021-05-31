using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherRobot_States : MonoBehaviour
{
    // Parametters
    
    [SerializeField]
    private float Aim_Time_Easy = 1.5f;
    [SerializeField]
    private float Aim_Time_Normal = 1.0f;
    [SerializeField]
    private float Aim_Time_Hard = 0.5f;
    // Variables 
    [HideInInspector]
    public GameObject Target;
    private Watcher_States Current_State = Watcher_States.Normal;
    private Watcher_Attacks Current_Attack = Watcher_Attacks.Aim;
    private Mob_Basic_Movement Watcher_Robot_Mo;
    private bool Can_Attack = true;
    private float Current_Aim_Time;
    private Watcher_Robot_Lazer Watcher_Robot_La;
    private float Max_View_Distance;
    private Watcher_Robot_Animation Watcher_Robot_An;
    // enums
    public enum Watcher_States
    {
        Normal,
        Agro,
        Dead
    }
    public enum Watcher_Attacks
    {
        Aim,
        Charge,
        Attack,
        Wait
    }
    // Start is called before the first frame update
    void Start()
    {
        Watcher_Robot_An = GetComponent<Watcher_Robot_Animation>();
        Watcher_Robot_La = GetComponentInChildren<Watcher_Robot_Lazer>();
        Watcher_Robot_Mo = GetComponent<Mob_Basic_Movement>();
        Target = Watcher_Robot_Mo.Target;
        Max_View_Distance = Watcher_Robot_Mo.Max_View_Distance;
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Aim_Time = Aim_Time_Hard;
                break;
            case 1:
                Current_Aim_Time = Aim_Time_Normal;
                break;
            default:
                Current_Aim_Time = Aim_Time_Easy;
                break;
        }
        // Do the update 
        Update_Attack(Current_Attack);
        Update_State(Current_State);
        // Pipe event subscription
        Event_System.current.onPipeEntered += Piped;
    }
    private void OnDestroy()
    {
        // Pipe event unsubscription
        Event_System.current.onPipeEntered -= Piped;
    }
    private void Piped()
    {
        Update_State(Watcher_States.Normal);
    }

    private void Update()
    {
        // Get distance from Target game object 
        float Distance_From_Target = Vector2.Distance(Target.transform.position, this.transform.position);

        // Check if the target is in reach distance from this mob
        if (Distance_From_Target < Max_View_Distance)
        {
            Update_State(Watcher_States.Agro);
        }
            
        if (Current_State == Watcher_States.Agro && Can_Attack)
        {
            StartCoroutine(Attack_Sequence());
        }
    }
    // Launch attacks action in sequence
    private IEnumerator Attack_Sequence()
    {
        // Block Any other sequences
        Can_Attack = false;
        Update_Attack(Watcher_Attacks.Aim);
        yield return new WaitForSeconds(Current_Aim_Time); // Wait until Aimed
        Update_Attack(Watcher_Attacks.Charge);
        yield return new WaitForSeconds(Current_Aim_Time * 0.2f); // Wait until Charged
        Update_Attack(Watcher_Attacks.Attack);
        Watcher_Robot_An.Launch_Attack_Animation();
        yield return new WaitForSeconds(0.1f); // Wait until Attack done
        Update_Attack(Watcher_Attacks.Wait);
        yield return new WaitForSeconds(Current_Aim_Time * 1.5f); // Wait 
        Can_Attack = true;
    }
    public void Update_State(Watcher_States State)
    {
        Current_State = State;
        switch (State)
        {
            case Watcher_States.Normal:
                break;
            case Watcher_States.Agro:
                break;
            case Watcher_States.Dead:
                // Disable Lazer and movement
                Watcher_Robot_Mo.Set_Life(false);
                Watcher_Robot_La.enabled = false;
                break;
            default:
                break;
        }
        Watcher_Robot_La.Update_State(State); // Update Lazer State
    }
    public void Update_Attack(Watcher_Attacks Attack)
    {
        Current_Attack = Attack;
        switch (Attack)
        {
            case Watcher_Attacks.Aim:
                break;
            case Watcher_Attacks.Charge:
                Watcher_Robot_Mo.Set_Life(false);// Stop Robot Movements
                break;
            case Watcher_Attacks.Attack:
                break;
            case Watcher_Attacks.Wait:
                Watcher_Robot_Mo.Set_Life(true);// Enable Robot Movements
                break;
            default:
                break;
        }
        Watcher_Robot_La.Update_Attack(Attack);
    }

    public void Be_Attacked()
    {
        Update_State(Watcher_States.Agro);
    }
}
