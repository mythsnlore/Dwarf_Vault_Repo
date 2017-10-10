
using System;
using UnityEngine;

public class WaitABitAction : GoapAction
{
    private bool waited = false;
    //private ForgeComponent targetForge; // where we forge tools

    private float startTime = 0;
    public float waitDuration = 5; // seconds

    public WaitABitAction () {
        //addPrecondition ("hasOre", true);
        //addPrecondition("hasFirewood", true);
        addEffect ("waitedABit", true);
    }


    public override void reset ()
    {
        waited = false;
        //targetForge = null;
        startTime = 0;
    }

    public override bool isDone ()
    {
        return waited;
    }

    public override bool requiresInRange ()
    {
        return false;
    }

    public override bool checkProceduralPrecondition (GameObject agent)
    {
        return true;
    }

    public override bool perform (GameObject agent)
    {
        BodyComponent body = (BodyComponent)agent.GetComponent(typeof(BodyComponent));
        body.statEnergy = Mathf.Min(100, body.statEnergy += ((body.fatigueRate * 1.2f) * Time.deltaTime)); //should add each frame perform is called

        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > waitDuration) {
            waited = true;
        }
        return true;
    }

}

