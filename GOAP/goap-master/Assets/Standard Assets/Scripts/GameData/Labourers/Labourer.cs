using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * A general labourer class.
 * You should subclass this for specific Labourer classes and implement
 * the createGoalState() method that will populate the goal for the GOAP
 * planner.
 */
public abstract class Labourer : MonoBehaviour, IGoap
{
	public BackpackComponent backpack;
    public BodyComponent body;
	public float moveSpeed;
    public float moveStartSpeed = 2;
    //public float fatigueRate;

    public HashSet<KeyValuePair<string,object>> worldDataMemory;
    public HashSet<KeyValuePair<string,object>> worldDataCurrent;

	void Start ()
	{
        moveSpeed = moveStartSpeed;
        //fatigueRate = 0.5f;

		if (backpack == null)
			backpack = gameObject.AddComponent <BackpackComponent>( ) as BackpackComponent;
		if (backpack.tool == null) {
			GameObject prefab = Resources.Load<GameObject> (backpack.toolType);
			GameObject tool = Instantiate (prefab, transform.position, transform.rotation) as GameObject;
			backpack.tool = tool;
			tool.transform.parent = transform; // attach the tool
		}

        if (body == null)
            body = gameObject.AddComponent <BodyComponent>() as BodyComponent;
	}


	void Update ()
	{
        float loadLevel;
        
        loadLevel = backpack.numFirewood + (backpack.numLogs * 5) + (backpack.numOre * 3);
        if (loadLevel >= 5)
            moveSpeed = moveStartSpeed/2;
        else
            moveSpeed = moveStartSpeed;

        body.statEnergy -= body.fatigueRate * Time.deltaTime;
	}

	/**
	 * Key-Value data that will feed the GOAP actions and system while planning.
	 */
	public HashSet<KeyValuePair<string,object>> getWorldState () {
		HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> (); //this section is where I should be searching all supplies and fires to give a value for tools and fuel

		worldData.Add(new KeyValuePair<string, object>("hasOre", (backpack.numOre > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasLogs", (backpack.numLogs > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasFirewood", (backpack.numFirewood > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasTool", (backpack.tool != null) ));
        worldData.Add(new KeyValuePair<string, object>("isHurt", (body.statHealth < 50) ));
        worldData.Add(new KeyValuePair<string, object>("isHungry", (body.statHunger < 50) ));
        worldData.Add(new KeyValuePair<string, object>("isTired", (body.statEnergy < 50) ));

        worldDataCurrent = worldData;
        if (worldDataMemory == null)
        {
            Debug.Log("Setting initial world data memory for " + gameObject.name);
            worldDataMemory = worldDataCurrent;
        }
		
        return worldData;
	}

	/**
	 * Implement in subclasses
	 */
	public abstract HashSet<KeyValuePair<string,object>> createGoalState ();


	public void planFailed (HashSet<KeyValuePair<string, object>> failedGoal)
	{
		// Not handling this here since we are making sure our goals will always succeed.
		// But normally you want to make sure the world state has changed before running
		// the same goal again, or else it will just fail.
        /*if (worldDataMemory != null)
        {
            if (worldDataCurrent != worldDataMemory)
            {
                
            }
        }*/
        Debug.Log("Plan Failed for " + gameObject.name + ", setting world data memory.");
        worldDataMemory = worldDataCurrent;
	}

	public void planFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
	{
		// Yay we found a plan for our goal
        Debug.Log ("<color=green>Plan found </color> "+ gameObject.name + " " +GoapAgent.prettyPrint(actions));
	}

	public void actionsFinished ()
	{
		// Everything is done, we completed our actions for this gool. Hooray!
        Debug.Log ("<color=blue>Actions completed </color>"+ gameObject.name);
	}

	public void planAborted (GoapAction aborter)
	{
		// An action bailed out of the plan. State has been reset to plan again.
		// Take note of what happened and make sure if you run the same goal again
		// that it can succeed.
        Debug.Log ("<color=red>Plan Aborted </color> "+ gameObject.name + " " +GoapAgent.prettyPrint(aborter));
	}

	public bool moveAgent(GoapAction nextAction) {
		// move towards the NextAction's target
		float step = moveSpeed * Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextAction.target.transform.position, step);
		
		if (gameObject.transform.position.Equals(nextAction.target.transform.position) ) {
			// we are at the target location, we are done
			nextAction.setInRange(true);
			return true;
		} else
			return false;
	}
}

