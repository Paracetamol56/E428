using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Prefab_Spawner : MonoBehaviour
{
    // Parametters
    [SerializeField]    
    private List<GameObject> Audio_List;
    
    public void Play_A_Sound(int Sound_Index)
    {
        // Check if the sound index is valid 
        if (Sound_Index >= 0 && Sound_Index < Audio_List.Count)
        {
            // Check if ther is a sound clip at the index
            if (Audio_List[Sound_Index] != null)
            {

                // Instanciate audio prefab
                Debug.Log("Play sound number " + Sound_Index);
                Instantiate(Audio_List[Sound_Index], transform);
            }
            
        }
    }
}
