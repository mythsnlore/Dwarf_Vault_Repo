
using System;
using UnityEngine;

public class PickUpFoodAction : GoapAction
{
    private bool isFed = false; // this is for COMPLETION
    private SupplyPileComponent targetSupplyPile; // where food is

    public PickUpFoodAction () {
        addPrecondition ("isHungry", true); // you must be hungry
        addEffect ("isHungry", false); // you are no longer hungry
    }


    public override void reset ()
    {
        isFed = false;
        targetSupplyPile = null;
    }

    public override bool isDone ()
    {
        return isFed; // maybe a better way to do this where eating is not always a one step solution?
    }

    public override bool requiresInRange ()
    {
        return true; // yes we need to be near a supply pile so we can pick up the logs
    }

    public override bool checkProceduralPrecondition (GameObject agent)
    {
        // find the nearest supply pile that has spare logs
        SupplyPileComponent[] supplyPiles = (SupplyPileComponent[]) UnityEngine.GameObject.FindObjectsOfType ( typeof(SupplyPileComponent) );
        SupplyPileComponent closest = null;
        float closestDist = 0;

        foreach (SupplyPileComponent supply in supplyPiles) {
            if (supply.numFood > 0) {
                if (closest == null) {
                    // first one, so choose it for now
                    closest = supply;
                    closestDist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
                } else {
                    // is this one closer than the last?
                    float dist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
                    if (dist < closestDist) {
                        // we found a closer one, use it
                        closest = supply;
                        closestDist = dist;
                    }
                }
            }
        }
        if (closest == null)
            return false;

        targetSupplyPile = closest;
        target = targetSupplyPile.gameObject;

        return closest != null;
    }

    public override bool perform (GameObject agent)
    {
        if (targetSupplyPile.numFood > 0) {
            targetSupplyPile.numFood -= 1;
            isFed = true;
            //TODO play effect, change actor icon
            //BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
            BodyComponent body = (BodyComponent)agent.GetComponent(typeof(BodyComponent));
            //backpack.numLogs = 1;
            body.statHunger = 100;

            return true;
        } else {
            // we got there but there was no logs available! Someone got there first. Cannot perform action
            return false;
        }
    }
}

