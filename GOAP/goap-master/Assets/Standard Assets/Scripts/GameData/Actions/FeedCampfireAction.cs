
using System;
using UnityEngine;

public class FeedCampfireAction: GoapAction
{
    private bool droppedOffFirewood = false; // used for completion
    private CampFireComponent targetCampfire; // where we drop off 

    public FeedCampfireAction () {
        addPrecondition ("hasFirewood", true);
        //addEffect ("hasFirewood", false); // try to do it without this and let world conditions take care of it
        addEffect ("feedTheFire", true);
    }


    public override void reset ()
    {
        droppedOffFirewood = false;
        targetCampfire = null;
    }

    public override bool isDone ()
    {
        return droppedOffFirewood;
    }

    public override bool requiresInRange ()
    {
        return true; // yes we need to be near a supply pile so we can drop off the logs
    }

    public override bool checkProceduralPrecondition (GameObject agent)
    {
        // find the nearest supply pile
        CampFireComponent[] campFires = (CampFireComponent[]) UnityEngine.GameObject.FindObjectsOfType ( typeof(CampFireComponent) );
        CampFireComponent closest = null;
        float closestDist = 0;

        foreach (CampFireComponent fire in campFires) {
            if (closest == null) {
                // first one, so choose it for now
                closest = fire;
                closestDist = (fire.gameObject.transform.position - agent.transform.position).magnitude;
            } else {
                // is this one closer than the last?
                float dist = (fire.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist) {
                    // we found a closer one, use it
                    closest = fire;
                    closestDist = dist;
                }
            }
        }
        if (closest == null)
            return false;

        targetCampfire = closest;
        NodeCheck[] nodes = closest.GetComponentsInChildren<NodeCheck>();
        NodeCheck thisNode = null;

        foreach (NodeCheck node in nodes)
        {
            if (node.gameObject.CompareTag("Node"))
            {
                if (node.occupied == false)
                    thisNode = node;
                else
                    return false;
            }
        }

        target = thisNode.gameObject; // this is where I could associate nodes as targets

        return closest != null; //I'm not sure why we do this
    }

    public override bool perform (GameObject agent)
    {
        BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
        BodyComponent body = (BodyComponent)agent.GetComponent(typeof(BodyComponent));
        targetCampfire.fuel += backpack.numFirewood;
        droppedOffFirewood = true;
        backpack.numFirewood = 0;
        body.statHunger -= 5;

        return true;
    }
}
