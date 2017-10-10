using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Woodsman : Labourer
{
    
    public CampFire_Component cFire;
    public float firewoodNum;

    public string goalText;
    
    void Start ()
    {
        cFire = UnityEngine.GameObject.Find("CampFire").GetComponent<CampFire_Component>();
    }

    void Update()
    {
        firewoodNum = cFire.PiecesRemainging;

        if (firewoodNum == 0)
        {
            goalText = "FirewoodToCampfire";
        }
        else
        {
            goalText = "collectLogs";
        }

        if (actionsDone)
        {
            createGoalState();
        }
    }

    public override HashSet<KeyValuePair<string,object>> createGoalState () {
        HashSet<KeyValuePair<string,object>> goal = new HashSet<KeyValuePair<string,object>> ();

        Debug.Log("Created Goal with " + goalText + "as the goal."); //createGoalState never seems to be called...

        goal.Add(new KeyValuePair<string, object>(goalText, true));
            return goal;

    }

}
