using UnityEngine;
using System.Collections;

public class NavigateToTarget : MonoBehaviour {

//    public Transform target;
    CamFollow CF;
    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    public float velZ;

    public bool drunk = false;
    public bool hasTarget;

	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        CF = FindObjectOfType<Camera>().GetComponentInParent<CamFollow>();
	}

    void Update () 
    {
        velZ = agent.velocity.z;
        hasTarget = CF.following;

        if (hasTarget)
        {
            agent.SetDestination(target.position);
        }
    }

//    void Deselect()
//    {
//        selected = false;
//    }

    public void SetDestination(Transform point) //they all move at once, make better system
    {
        agent.SetDestination(point.position);
    }
}
