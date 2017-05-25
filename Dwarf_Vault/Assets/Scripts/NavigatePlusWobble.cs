using UnityEngine;
using System.Collections;

public class NavigatePlusWobble : MonoBehaviour {

    public Vector3 me;
    Rigidbody rb;
    public UnityEngine.AI.NavMeshAgent agent;

    public bool drunk;

    public float wobble;
    public float wobbleMag;
    public float freq = 5f;

    public float zVel;
    public float tip;

    void Start ()
    {
        me = transform.localPosition;
        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update ()
    {
        zVel = agent.velocity.z;
        wobble = Mathf.Clamp(agent.remainingDistance, 0, 1) * wobbleMag;
        tip = -transform.localPosition.x;

        if (drunk)
        {
            me = transform.localPosition;
            //will only wobble in x, not local x
            transform.localPosition = Vector3.Lerp(me, new Vector3(Mathf.Sin(Time.time * freq) * Random.Range(wobble/2,wobble), 0, 0), Time.deltaTime);
        }

        if (agent.remainingDistance < 2)
        {
            transform.localPosition = Vector3.Lerp(me, new Vector3(0, 0, 0), Time.deltaTime * 2);
            transform.Rotate(Vector3.Lerp(me, new Vector3(0, 0, 0), Time.deltaTime * 2));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, tip * 1),Space.World);
        }

    }
}