using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Save_System : MonoBehaviour
{
    // Start is called before the first frame update
    public static void Save_Data(int Level_Buid_Index)
    {
        PlayerPrefs.SetInt("Difficulty", Global_Variable.Difficulty_Level);
        PlayerPrefs.SetInt("Last_Level", Global_Variable.Last_Level_Build_Index);
    }
    public static void Recover_Data()
    {
        Global_Variable.Difficulty_Level = PlayerPrefs.GetInt("Difficulty", 1);
        Global_Variable.Last_Level_Build_Index = PlayerPrefs.GetInt("Last_Level", 0);
    }
    public static void Wipe_Data()
    {
        PlayerPrefs.DeleteAll();
    }
}
