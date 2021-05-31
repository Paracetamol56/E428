using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher_Robot_Lazer : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private GameObject Rotator;
    [SerializeField]
    private float Aim_Speed = 2;
    [SerializeField]
    private Watcher_Robot_RayCast Watcher_Robot_RC;
    // Variables
    private GameObject Target;
    private Mob_Basic_Movement Watcher_Robot_Mo;
    
    private WatcherRobot_States.Watcher_Attacks Attack_State = WatcherRobot_States.Watcher_Attacks.Aim;
    private WatcherRobot_States.Watcher_States State = WatcherRobot_States.Watcher_States.Normal;
    private float Rotation_Angle = 0;
    private Vector3 Aim_Vector;

    // Start is called before the first frame update
    void Start()
    {
        // Get Movement script and get target
        Watcher_Robot_Mo = GetComponentInParent<Mob_Basic_Movement>();
        //Watcher_Robot_RC = GetComponentInChildren<Watcher_Robot_RayCast>();
        Target = Watcher_Robot_Mo.Target;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == WatcherRobot_States.Watcher_States.Agro)
        {
            switch (Attack_State)
            {
                case WatcherRobot_States.Watcher_Attacks.Aim:
                    Aim_At_Target();
                    break;
                case WatcherRobot_States.Watcher_Attacks.Charge:
                    break;
                case WatcherRobot_States.Watcher_Attacks.Attack:

                    break;
                case WatcherRobot_States.Watcher_Attacks.Wait:
                    break;
                default:
                    break;
            }
        }
    }
    
    public void Update_State(WatcherRobot_States.Watcher_States state)
    {
        State = state;
        Watcher_Robot_RC.Update_State(state);
    }
    public void Update_Attack(WatcherRobot_States.Watcher_Attacks Attack)
    {
        Attack_State = Attack;
        Watcher_Robot_RC.Update_Attack(Attack);
    }
    
    private void Aim_At_Target()
    {
        // Find the optimal aim vector
        Vector3 New_Aim_Vecor = Target.transform.position - transform.position;
        // Lerp between the original aim and the optimal aim
        Aim_Vector = Vector3.Lerp(Aim_Vector, New_Aim_Vecor, Time.deltaTime * Aim_Speed);

        // Convert the Vector to a deg float
        Rotation_Angle = Mathf.Atan2(Aim_Vector.y, Aim_Vector.x) * Mathf.Rad2Deg;
        // Rotate the rotation axis accordingly
        Rotator.transform.eulerAngles = new Vector3(0, 0, Rotation_Angle);
    }
}
