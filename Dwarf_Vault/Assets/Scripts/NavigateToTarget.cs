using UnityEngine;
using System.Collections;

public class NavigateToTarget : MonoBehaviour {

    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    public float velZ;

    public bool drunk = false;

	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	void Update ()
    {
        agent.SetDestination(target.position);

        velZ = agent.velocity.z;

        if (drunk)
        {
            agent.Move( new Vector3(Random.Range(-3, 4), 0, 0));
        }
	}
}
