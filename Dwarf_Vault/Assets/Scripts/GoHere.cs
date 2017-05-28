using UnityEngine;
using System.Collections;

public class GoHere : MonoBehaviour {

    public bool hasTarget;

    void Update()
    {
       hasTarget = FindObjectOfType<Camera>().GetComponentInParent<CamFollow>().following;

        if (Input.GetMouseButtonDown(0))
        {
            if (hasTarget == true)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        if (hit.transform.gameObject.tag == "Ground")
                        {
                            transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        }
                    }
                }
            }
        }
    }
}
