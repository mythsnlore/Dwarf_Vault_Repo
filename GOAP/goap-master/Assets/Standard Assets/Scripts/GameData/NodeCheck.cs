using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCheck : MonoBehaviour 
{
    private BoxCollider col;
    public GameObject user;
    public bool occupied;

    void Start ()
    {
        col = GetComponent<BoxCollider>();
        occupied = false;
    }
        
    void OnTriggerEnter (Collider other)
    {
        if (!occupied)
        {
            if (other.CompareTag("Actor"))
            {
                occupied = true;
                user = other.gameObject;
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Actor"))
        {
            if (other.gameObject == user)
            {
                occupied = false;
                user = null;
            }
        }
    }

}
