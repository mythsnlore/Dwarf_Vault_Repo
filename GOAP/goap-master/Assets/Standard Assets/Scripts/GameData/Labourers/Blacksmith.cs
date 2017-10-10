using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blacksmith : Labourer
{
	/**
	 * Our only goal will ever be to make tools.
	 * The ForgeTooldAction will be able to fulfill this goal.
	 */

	public override HashSet<KeyValuePair<string,object>> createGoalState ()
    {
        HashSet<KeyValuePair<string,object>> goal = new HashSet<KeyValuePair<string,object>>();

        GameObject agent = this.gameObject;
        BodyComponent body = (BodyComponent)agent.GetComponent(typeof(BodyComponent));
		
        CampFireComponent[] campFires = (CampFireComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(CampFireComponent));
        SupplyPileComponent[] supplies = (SupplyPileComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(SupplyPileComponent));
        //CampFireComponent closest = null;
        //float closestDist = 0;
        goal.Clear();

        if (campFires.Length > 0) //if there are any fires
        {
            foreach (CampFireComponent fire in campFires)
            {
                if (fire.fuel <= 3) //if fire is low
                {
                    goal.Add(new KeyValuePair<string, object>("feedTheFire", true));
                    return goal;
                }
                else
                {
                    Debug.Log("Campires checked, no need to give firewood");
                    goto Next;
                }
            }
        }

        Next:

        if (body.statHunger <= 50) //if you're hungry
            goal.Add(new KeyValuePair<string, object>("isHungry", false));
        else
        {
            if (supplies.Length > 0) //if there are any supplies
            {
                foreach (SupplyPileComponent supply in supplies)
                {
                    if (supply.numTools <= 1) //if supplies are low
                    {
                        goal.Add(new KeyValuePair<string, object>("collectTools", true));
                        return goal;
                    }
                }
                Debug.Log("No goal conditions met, waiting instead.");
                goal.Add(new KeyValuePair<string, object>("waitedABit", true));
                return goal;
                //return null;
            }
        }
    
        return goal; 
    }
}

