using System.Collections.Generic;
using UnityEngine;

public class CampFireComponent : MonoBehaviour 
{
    public float fuel;
    public bool isLit;

    Animator CFanim;

    void Start ()
    {
        CFanim = GetComponentInChildren<Animator>();
    }

    void Update ()
    {
        if (fuel > 0)
            isLit = true;
        else
            isLit = false;

        if (isLit)
            CFanim.SetBool("FireLit", true);
        else
            CFanim.SetBool("FireLit", false);
        
        if (fuel > 0)
            fuel = Mathf.Max(0, (fuel - (0.1f * Time.deltaTime)));
        else
            fuel = 0;
    }
}
