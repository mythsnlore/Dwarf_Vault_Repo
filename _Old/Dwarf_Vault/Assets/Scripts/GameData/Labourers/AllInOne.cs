using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllInOne : Labourer
{
	/**
	 * Our only goal will ever be to chop logs.
	 * The ChopFirewoodAction will be able to fulfill this goal.
	 */
	public override HashSet<KeyValuePair<string,object>> createGoalState () {
		HashSet<KeyValuePair<string,object>> goal = new HashSet<KeyValuePair<string,object>> ();

		if (Random.value > 0.5)
		{
			goal.Add(new KeyValuePair<string, object>("collectFirewood", true));
		}
		else
		{
			goal.Add(new KeyValuePair<string, object>("collectOre", true));
		}
		return goal;
	}
}