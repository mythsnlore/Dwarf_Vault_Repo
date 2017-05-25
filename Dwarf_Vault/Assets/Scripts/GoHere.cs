using UnityEngine;
using System.Collections;

public class GoHere : MonoBehaviour {

	void Update () 
    {
        if(Input.GetMouseButtonDown(0))
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
