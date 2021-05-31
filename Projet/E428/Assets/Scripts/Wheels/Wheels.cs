using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    public float Wheel_Speed = 1;
    private Animator Wheel_AC;
    // Start is called before the first frame update
    void Start()
    {
        Wheel_AC = GetComponent<Animator>();
        Wheel_AC.SetFloat("Speed", Wheel_Speed);
    }
}
