using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeByY : MonoBehaviour {

    public Material myMat;
    public Transform mainCam;
    float dist;
    public float maxDist;
    float camPos;
    public float myRange;
    Color dCol;

	void Start () {
        myMat = gameObject.GetComponent<Renderer>().material as Material;
        mainCam = FindObjectOfType<Camera>().transform;

        dCol = myMat.GetColor("_Color");
	}
	

	void Update () {

        camPos = mainCam.position.y;
        dist = Mathf.Abs(camPos - transform.position.y);

        if (dist > myRange && dist < maxDist) //object needs to fade
        {
            float fade = Mathf.Min(1 / (dist - myRange), 1);
            myMat.color = new Color(dCol.r, dCol.g, dCol.b, fade);
        }
        else if (dist > myRange)
        {
            myMat.color = new Color(dCol.r, dCol.g, dCol.b, 0);
        }
        else
        {
            myMat.color = new Color(dCol.r, dCol.g, dCol.b, 1);
        }
            

        //I want the object to fade out when the distace passes a threashold
        //If the number is positive, the object is below the camera level(visible or below plane)
        //If the number is negative, the object is above the camera level(possibly visible as ghost)

        // 1.0 = 10/10
        // 0.5 = 10/20


	}
}
