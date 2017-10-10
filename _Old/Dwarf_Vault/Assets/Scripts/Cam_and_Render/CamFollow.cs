using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public Transform target;
    public float FollowSpeed;
    public float MoveSpeed;
    public float RotSpeed;
    public float ZoomSpeed;
    Camera cam;
    Vector3 offset;

    public bool following = false;

	void Start () 
    {
        offset = target.position - transform.position;
        cam = gameObject.GetComponentInChildren<Camera>();
	}
	

	void Update ()
    {
        transform.Rotate(transform.rotation.x, ((Input.GetAxis("Rotate")) * RotSpeed), transform.rotation.z);

        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0)
        {
            cam.fieldOfView -= (Input.GetAxis("Mouse ScrollWheel")) * ZoomSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.transform.gameObject.tag == "NavAgent")
                    {
                        target = hit.transform;
                        following = true;
//                        hit.transform.gameObject.GetComponentInParent<NavigateToTarget>().selected = true;
                    }
//                    else if (hit.transform.gameObject.tag == "Ground")
//                    {
//                        target = null;
//                        following = false;
//                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            target = null;
            following = false;
        }

        if (following)
        {
            transform.position = Vector3.Lerp(transform.position, (target.position - offset), Time.deltaTime * FollowSpeed);
//            transform.Rotate(transform.rotation.x, ((Input.GetAxis("Horizontal")) * RotSpeed), transform.rotation.z);
//            cam.fieldOfView -= (Input.GetAxis("Vertical")) * ZoomSpeed;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 | Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                //target = null;
                following = false;
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * MoveSpeed);
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * MoveSpeed);
        }

//        transform.Rotate(transform.rotation.x, ((Input.GetAxis("Horizontal")) * RotSpeed), transform.rotation.z);
//        cam.fieldOfView -= (Input.GetAxis("Vertical")) * ZoomSpeed; //this broke for some reason
    }
}
