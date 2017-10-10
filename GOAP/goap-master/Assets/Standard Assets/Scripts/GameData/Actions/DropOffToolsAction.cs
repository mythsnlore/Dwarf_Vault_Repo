
using System;
using UnityEngine;

public class DropOffToolsAction : GoapAction
{
	private bool droppedOffTools = false;
	private SupplyPileComponent targetSupplyPile; // where we drop off the  tools
	
	public DropOffToolsAction () {
		addPrecondition ("hasNewTools", true); // can't drop off tools if we don't already have some
		addEffect ("hasNewTools", false); // we now have no tools
		addEffect ("collectTools", true); // we collected tools
	}
	
	
	public override void reset ()
	{
		droppedOffTools = false;
		targetSupplyPile = null;
	}
	
	public override bool isDone ()
	{
		return droppedOffTools;
	}
	
	public override bool requiresInRange ()
	{
		return true; // yes we need to be near a supply pile so we can drop off the tools
	}
	
	public override bool checkProceduralPrecondition (GameObject agent)
	{
		// find the nearest supply pile that has spare tools
		SupplyPileComponent[] supplyPiles = (SupplyPileComponent[]) UnityEngine.GameObject.FindObjectsOfType ( typeof(SupplyPileComponent) );
		SupplyPileComponent lowest = null;
		float lowestNum = 0;
		
		foreach (SupplyPileComponent supply in supplyPiles) {
            if (supply.numTools <= 2)
            {
                if (lowest == null)
                {
                    // first one, so choose it for now
                    lowest = supply;
                    lowestNum = supply.numTools;
                }
                else
                {
                    // is this one closer than the last?
                    float toolNum = supply.numTools;
                    if (toolNum < lowestNum)
                    {
                        // we found a closer one, use it
                        lowest = supply;
                        lowestNum = toolNum;
                    }
                }
            }
		}
		if (lowest == null)
			return false;

		targetSupplyPile = lowest;
		target = targetSupplyPile.gameObject;
		
		return lowest != null;
	}
	
	public override bool perform (GameObject agent)
	{
		targetSupplyPile.numTools += 2;
		droppedOffTools = true;
		//TODO play effect, change actor icon
		
		return true;
	}
}