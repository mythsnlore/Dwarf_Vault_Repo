using UnityEngine;
using System.Collections;

public class RightSelf : MonoBehaviour {

    //public Vector3 startPos;
    //public Vector3 startRot;
    //public Vector3 startSca;

    public Transform start;

    public float speed;

	void Start () 
    {
        //startPos = transform.localPosition;
        //startRot = transform.localRotation.eulerAngles;
        //startSca = transform.localScale;
        start = transform;
	}
	
	void Update () 
    {
        if (transform.position != start.position)
        {
            transform.position = Vector3.Lerp(transform.position, start.position, Time.deltaTime * speed);
            Debug.Log("Correcting Pos");
        }
	}
}
