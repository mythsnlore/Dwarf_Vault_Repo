using UnityEngine;
using System.Collections;

public class FeedFirewoodAction : GoapAction
{
	private bool fedFirewood = false;
	private CampFire_Component targetCampfire; // where we drop off the firewood

	public FeedFirewoodAction () {
		addPrecondition ("hasFirewood", true); // can't drop off firewood if we don't already have some
		addEffect ("hasFirewood", false); // we now have no firewood
        addEffect ("FirewoodToCampfire", true); // we fed the fire
	}


	public override void reset ()
	{
		fedFirewood = false;
		targetCampfire = null;
	}

	public override bool isDone ()
	{
		return fedFirewood;
	}

	public override bool requiresInRange ()
	{
		return true; // yes we need to be near a camp pile so we can drop off the firewood
	}

	public override bool checkProceduralPrecondition (GameObject agent)
	{
		// find the nearest camp pile that has spare firewood
		CampFire_Component[] campFires = (CampFire_Component[]) UnityEngine.GameObject.FindObjectsOfType ( typeof(CampFire_Component) );
		CampFire_Component closest = null;
		float closestDist = 0;

		foreach (CampFire_Component camp in campFires) {
			if (closest == null) {
				// first one, so choose it for now
				closest = camp;
				closestDist = (camp.gameObject.transform.position - agent.transform.position).magnitude;
			} else {
				// is this one closer than the last?
				float dist = (camp.gameObject.transform.position - agent.transform.position).magnitude;
				if (dist < closestDist) {
					// we found a closer one, use it
					closest = camp;
					closestDist = dist;
				}
			}
		}
		if (closest == null)
			return false;

		targetCampfire = closest;
		target = targetCampfire.gameObject;

		return closest != null;
	}

	public override bool perform (GameObject agent)
	{
		BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
        backpack.numFirewood += targetCampfire.PiecesRemainging;
		fedFirewood = true;
		backpack.numFirewood = 0;

		return true;
	}
}

