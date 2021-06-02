using UnityEngine;

public class Electricity_Manager_Movement : MonoBehaviour
{
    // Parametters
    [SerializeField]
    private float Easy_Speed = 4f;
    [SerializeField]
    private float Normal_Speed = 5f;
    [SerializeField]
    private float Hard_Speed = 6.5f;
    [SerializeField]
    private float Fly_Amplitude = 3f;
    [SerializeField]
    private float Lerp_Speed = 3f;
    [SerializeField]
    private float Death_Lerp_Speed = 3f;
    // Variables
    private float Current_Speed;
    private Vector3 Spawn_Position;
    private Boss_States State = Boss_States.Attack;
    private float Raw_Y_Potential_Position = 0;
    private float Targeted_Y_Position;
    // Start is called before the first frame update
    void Start()
    {
        switch (Global_Variable.Difficulty_Level)
        {
            case 2:
                Current_Speed = Hard_Speed;
                break;
            case 1:
                Current_Speed = Normal_Speed;
                break;
            default:
                Current_Speed = Easy_Speed;
                break;
        }
        // Get the spawn transition
        Spawn_Position = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // Add To the potential position 
        Raw_Y_Potential_Position += Current_Speed * Time.deltaTime;
        // If Boss Is in combat
        if (State == Boss_States.Attack || State == Boss_States.Wait)
        {
           
            //  Current Y position is equal to cos of Raw_Y_Potential_Position mapped to 0 to 1 muliplied by the amplitude + th og y position
            Targeted_Y_Position = (Mathf.Sin(Raw_Y_Potential_Position) / 2 + 0.5f)* Fly_Amplitude + Spawn_Position[1];
            Lerp_To_Posision(new Vector3(transform.position.x, Targeted_Y_Position), Lerp_Speed);
        }
        else if (State == Boss_States.Cinematic)
        {
            //  Current Y position is equal to cos of Raw_Y_Potential_Position mapped to 0 to 1 muliplied by the amplitude + th og y position
            Targeted_Y_Position = (Mathf.Sin(Raw_Y_Potential_Position) / 2 + 0.5f) * Fly_Amplitude/3 + Spawn_Position[1];
            Lerp_To_Posision(new Vector3(transform.position.x, Targeted_Y_Position), Lerp_Speed);
        }
        else // Dead state
        {
            // Do the thing
            Lerp_To_Posision(new Vector3(transform.position.x, -1), Death_Lerp_Speed);
        }
    }
    public void Update_State(Boss_States state)
    {
        State = state;
        Debug.Log("Electricity Manager Mo state = " + State);
    }
    public void Lerp_To_Posision(Vector2 Position, float lerpSpeed)
    {
        float Actual_X = Mathf.Lerp(transform.position.x, Position.x, lerpSpeed * Time.deltaTime);
        float Actual_Y = Mathf.Lerp(transform.position.y, Position.y, lerpSpeed * Time.deltaTime);
        transform.position = new Vector3(Actual_X, Actual_Y, 0);
    }
}
