using UnityEngine;
using System.Collections;

public class WallBehavior : MonoBehaviour {

    bool Clicked = false;

    void LateUpdate ()
    {
        if (Clicked)
        {
            ToggleWall();
        }
    }

    void OnMouseDown () 
    {
        Clicked = true;
    }

    void ToggleWall ()
    {
        Debug.Log("Wall Clicked");
        gameObject.SetActive(false);
        //Clicked = false;
    }
}
