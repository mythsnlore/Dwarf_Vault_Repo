using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public Transform target;
    public float FollowSpeed;
    public float RotSpeed;
    public float ZoomSpeed;
    Camera cam;
    Vector3 offset;


	void Start () 
    {
        offset = target.position - transform.position;
        cam = gameObject.GetComponentInChildren<Camera>();
	}
	

	void Update () 
    {
        transform.position = Vector3.Lerp(transform.position, (target.position - offset), Time.deltaTime * FollowSpeed);

        transform.Rotate(transform.rotation.x, ((Input.GetAxis("Horizontal")) * RotSpeed), transform.rotation.z);

        cam.fieldOfView -= (Input.GetAxis("Vertical")) * ZoomSpeed;
	}
}
