using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCutter : Labourer
{
	/**
	 * Our only goal will ever be to chop logs.
	 * The ChopFirewoodAction will be able to fulfill this goal.
	 */
	public override HashSet<KeyValuePair<string,object>> createGoalState () {
		HashSet<KeyValuePair<string,object>> goal = new HashSet<KeyValuePair<string,object>> ();
		
        GameObject agent = this.gameObject;
        BodyComponent body = (BodyComponent)agent.GetComponent(typeof(BodyComponent));

        if (body.statHunger <= 50)
            goal.Add(new KeyValuePair<string, object>("isHungry", false ));
        else
		    goal.Add(new KeyValuePair<string, object>("collectFirewood", true ));
		
        return goal;
	}
}

