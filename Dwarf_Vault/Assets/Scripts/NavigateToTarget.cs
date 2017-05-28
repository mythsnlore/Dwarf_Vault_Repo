using UnityEngine;
using System.Collections;

public class NavigateToTarget : MonoBehaviour {

    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    public float velZ;

    public bool drunk = false;
    public bool selected = false;

	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
//	void Update ()
//    {
//        agent.SetDestination(target.position);
//
//        velZ = agent.velocity.z;
//
//        if (drunk)
//        {
//            agent.Move( new Vector3(Random.Range(-3, 4), 0, 0));
//        }
//	}
//
//    public bool selected = false;

    void Update () 
    {
        velZ = agent.velocity.z;

        if (selected)
        {
            agent.SetDestination(target.position);
//                RaycastHit hit;
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                if (Physics.Raycast(ray, out hit))
//                {
//                    if (hit.collider != null)
//                    {
//                        if (hit.transform.gameObject.tag == "Ground")
//                        {
//                            transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
//                        }
//                    }
//

            }

        if (Input.GetMouseButtonDown(1))
        {
            selected = false;
        }
    }
}
