using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher_Robot_RayCast : MonoBehaviour
{
    // Parametters
    [SerializeField]
    LayerMask Lazer_Mask;
    [SerializeField]
    LineRenderer Lazer_Line;
    [SerializeField]
    Gradient Gradiant_Scan;
    [SerializeField]
    Gradient Gradiant_Found;
    [SerializeField]
    Gradient Gradiant_Attack;
    [SerializeField]
    Gradient Gradiant_Invisible;
    // Variable
    private WatcherRobot_States.Watcher_Attacks Attack_State = WatcherRobot_States.Watcher_Attacks.Aim;
    private WatcherRobot_States.Watcher_States State = WatcherRobot_States.Watcher_States.Normal;
     
    // Update is called once per frame
    void Update()
    {
        if (State == WatcherRobot_States.Watcher_States.Agro)
        {
            Lazer_Line.enabled = true;
            switch (Attack_State)
            {
                case WatcherRobot_States.Watcher_Attacks.Aim:
                    Scan();
                    break;
                case WatcherRobot_States.Watcher_Attacks.Charge:
                    Lazer_Line.colorGradient = Gradiant_Invisible;
                    break;
                case WatcherRobot_States.Watcher_Attacks.Attack:
                    Attack();
                    break;
                case WatcherRobot_States.Watcher_Attacks.Wait:
                    Lazer_Line.colorGradient = Gradiant_Invisible;
                    break;
                default:
                    break;
            }
        }
        else
        {
            Lazer_Line.enabled = false;
        }
        
        
        
    }
    private void Scan()
    {
        RaycastHit2D Raycast_Info = Physics2D.Raycast(transform.position, transform.right,50,Lazer_Mask);
        if (Raycast_Info.collider !=null)
        {
            // Change Lazer Lenght
            Lazer_Line.SetPosition(1, new Vector3(Raycast_Info.distance,0,0));
        }
        IAttackable enemy = Raycast_Info.collider.GetComponent<IAttackable>();
        // Show if enemy is locked
        if (enemy != null)
        {
            Lazer_Line.colorGradient = Gradiant_Found;
        }
        else
        {
            Lazer_Line.colorGradient = Gradiant_Scan;
        }
    }
    private void Attack()
    {
        RaycastHit2D Raycast_Info = Physics2D.Raycast(transform.position, transform.right, 50, Lazer_Mask);
        IAttackable enemy = Raycast_Info.collider.GetComponent<IAttackable>();
        if (enemy != null)
        {
            // Change Lazer Lenght
            Lazer_Line.SetPosition(1, new Vector3(Raycast_Info.distance, 0, 0));
            enemy.Be_Attacked();
        }
        Lazer_Line.colorGradient = Gradiant_Attack;
    }
    // Used to update attack state
    public void Update_Attack(WatcherRobot_States.Watcher_Attacks Attack)
    {
        // Do the attack 
        Attack_State = Attack;

    }
    // Used to update state
    public void Update_State(WatcherRobot_States.Watcher_States state)
    {
        State = state;
    }

}
