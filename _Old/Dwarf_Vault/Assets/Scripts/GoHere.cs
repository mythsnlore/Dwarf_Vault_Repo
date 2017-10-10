using UnityEngine;
using System.Collections;

public class GoHere : MonoBehaviour {

    CamFollow CF;
    //public NavigateToTarget NTT;
    public bool hasTarget;

    void Start()
    {
        CF = FindObjectOfType<Camera>().GetComponentInParent<CamFollow>();
    }

    void Update()
    {

        hasTarget = CF.following;
        //NTT = CF.target.GetComponent<NavigateToTarget>(); //didn't work

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
                            //NTT.SetDestination(hit.transform);
                        }
                    }
                }
            }
        }
    }
}
