using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric_Robot_Attack : MonoBehaviour, IAttackable
{
    // Parameters
    [SerializeField]
    private GameObject Attack_To_Spawn;
    [SerializeField]
    private float Max_Attack_Distance = 10;
    [SerializeField]
    private float Easy_Attack_Delay = 2f;
    [SerializeField]
    private float Normal_Attack_Delay = 1.5f;
    [SerializeField]
    private float Hard_Attack_Delay = 1f;
    // Variables
    private GameObject Target;
    private Mob_Basic_Movement Mob_Basic_Mo;
    private Animator Mob_An;
    private float Max_View_Distance;
    private bool Is_Agro = false;
    private float Current_Attack_Delay;
    private bool Can_Attack = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get the target and the view distance of the movement component
        Mob_Basic_Mo = GetComponent<Mob_Basic_Movement>();
        Mob_An = GetComponent<Animator>();
        Target = Mob_Basic_Mo.Target;
        Max_View_Distance = Mob_Basic_Mo.Max_View_Distance;
        // Subscribe to the pipe event to Unagro the mob when the target is in a pipe
        Event_System.current.onPipeEntered += Unagro_Target;
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Attack_Delay = Hard_Attack_Delay;
                break;
            case 1:
                Current_Attack_Delay = Normal_Attack_Delay;
                break;
            default:
                Current_Attack_Delay = Easy_Attack_Delay;
                break;
        }

    }
    private void OnDestroy()
    {
        // Unsubscribe to prevent null reference errors
        Event_System.current.onPipeEntered -= Unagro_Target;
    }
    // Update is called once per frame
    void Update()
    {
        // Get distance from Target game object 
        float Distance_From_Target = Vector2.Distance(Target.transform.position, this.transform.position);

        // Check if the target is in reach distance from this mob
        if (Distance_From_Target < Max_View_Distance)
            Is_Agro = true;
        // If the mobis Agro and the target is in reach and the mob isn't waiting for his attack 
        if (Is_Agro && Distance_From_Target < Max_Attack_Distance && Can_Attack)
        {
            StartCoroutine(Attack_Action());
        }

    }
    private IEnumerator Attack_Action()
    {
        
        
        Can_Attack = false;
        yield return new WaitForSeconds(Current_Attack_Delay);
        // Prevent attack if the mob isn't agro after the delay
        if (Is_Agro && Mob_Basic_Mo.Get_Life())
        {
            // Launch Cast Animation
            Mob_An.SetTrigger("Cast");
            Instantiate(Attack_To_Spawn, Target.transform.position,Target.transform.rotation,null);
        }
        Can_Attack = true;
    }



    // Unagro the mob
    public void Unagro_Target()
    {
        Is_Agro = false;
    }
    // Agro The Mob
    public void Be_Attacked()
    {
        Is_Agro = true;
    }
}
