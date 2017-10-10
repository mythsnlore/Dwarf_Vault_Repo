using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public float startHealth;
    public float startHunger;
    public float startHappiness;
    public float startAnnoyance;
    public float startEnergy;
    public float startDrunken;

    [Range(0.0f, 100.0f)]
    public float healthLVL;
    [Range(0.0f, 100.0f)]
    public float hungerLVL;
    [Range(0.0f, 100.0f)]
    public float happinessLVL;
    [Range(0.0f, 100.0f)]
    public float annoyanceLVL;
    [Range(0.0f, 100.0f)]
    public float energyLVL;
    [Range(0.0f, 100.0f)]
    public float drunkenLVL;

    public enum status{ zero, critical, normal, maxed};

    public status health;
    public status hunger;
    public status happiness;
    public status energy;
    public status drunken;

    public bool isDead, isKO, isAsleep, isAngry, isDrunk;
}

public class Personality : MonoBehaviour 
{

    public Stats myStats;
    private float TimeStep = 0f;

//    Dictionary<string, float> StatDict =
//        new Dictionary<string, float>();
//
//    StatDict.Add("health", myStats.healthLVL);

	// Use this for initialization
	void Start () 
    {
        myStats.startHealth = 100;
        myStats.startHunger = 0;
        myStats.startHappiness = 100;
        myStats.startAnnoyance = 0;
        myStats.startEnergy = 100;
        myStats.startDrunken = 50;

        ResetStats();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
        TimeStep -= Time.deltaTime;

        if (TimeStep <= 0)
        {
            myStats.hungerLVL += 1f;
            myStats.energyLVL -= 1f;
            myStats.annoyanceLVL -= 1f;

            //convert lvls to enums

            if (myStats.healthLVL <= 0)             //health lvl to enums
                myStats.health = Stats.status.zero;
            else if (myStats.healthLVL < 30)
                myStats.health = Stats.status.critical;
            else if (myStats.healthLVL < 100)
                myStats.health = Stats.status.normal;
            else if (myStats.healthLVL >= 100)
                myStats.health = Stats.status.maxed;

            if (myStats.hungerLVL >= 100)           //hunger lvl to enums
                myStats.hunger = Stats.status.maxed;
            else if (myStats.hungerLVL > 50)
                myStats.hunger = Stats.status.critical;
            else if (myStats.hungerLVL > 10)
                myStats.hunger = Stats.status.normal;
            else if (myStats.hungerLVL <= 10)
                myStats.hunger = Stats.status.zero;

            if (myStats.happinessLVL >= 80)         //happiness lvl to enums
                myStats.happiness = Stats.status.maxed;
            else if (myStats.happinessLVL < 80)
                myStats.happiness = Stats.status.normal;
            else if (myStats.happinessLVL < 20)
                myStats.happiness = Stats.status.critical;
            else if (myStats.happinessLVL <= 0)
                myStats.happiness = Stats.status.zero;

            if (myStats.annoyanceLVL >= 80)         //annoyance lvl to enums
                myStats.happinessLVL -= 1;

            if (myStats.energyLVL <= 0)             //energy lvl to enums
                myStats.energy = Stats.status.zero;
            else if (myStats.energyLVL < 20)
                myStats.energy = Stats.status.critical;
            else if (myStats.energyLVL < 80)
                myStats.energy = Stats.status.normal;
            else if (myStats.energyLVL >= 80)
                myStats.energy = Stats.status.maxed;

            if (myStats.drunkenLVL <= 0)             //drunken lvl to enums
                myStats.drunken = Stats.status.zero;
            else if (myStats.drunkenLVL < 20)
                myStats.drunken = Stats.status.critical;
            else if (myStats.drunkenLVL < 80)
                myStats.drunken = Stats.status.normal;
            else if (myStats.drunkenLVL >= 80)
                myStats.drunken = Stats.status.maxed;

            //enums affect bools and cross-stat increase and decrease

            if (myStats.health == Stats.status.zero)
                myStats.isDead = true;

            if (myStats.hunger == Stats.status.maxed)
            {
                myStats.healthLVL -= 1f;
            }
            else if (myStats.hunger == Stats.status.zero)
            {
                myStats.healthLVL += 1f;
            }
                
            if (myStats.happiness == Stats.status.maxed)
                myStats.annoyanceLVL -= 1f;

            if (myStats.drunken == Stats.status.maxed)
            {
                myStats.energyLVL -= 1f;
            }
            else if (myStats.drunken == Stats.status.zero)
            {
                myStats.happinessLVL -= 5f;
            }

            myStats.healthLVL = Mathf.Clamp(myStats.healthLVL,0,100);
            myStats.hungerLVL = Mathf.Clamp(myStats.hungerLVL,0,100);
            myStats.happinessLVL = Mathf.Clamp(myStats.happinessLVL,0,100);
            myStats.annoyanceLVL = Mathf.Clamp(myStats.annoyanceLVL,0,100);
            myStats.energyLVL = Mathf.Clamp(myStats.energyLVL,0,100);
            myStats.drunkenLVL = Mathf.Clamp(myStats.drunkenLVL,0,100);

            TimeStep = 5f;
        }

        if (myStats.health == Stats.status.zero)
            myStats.isDead = true;

        if (myStats.happiness == Stats.status.critical | myStats.happiness == Stats.status.zero)
            myStats.isAngry = true;
        else if (myStats.happiness == Stats.status.normal | myStats.happiness == Stats.status.maxed)
            myStats.isAngry = false;

        if (myStats.energy == Stats.status.zero)
            myStats.isKO = true;

        if (myStats.drunken == Stats.status.maxed)
        {
            myStats.isDrunk = true;
        }
        else if (myStats.drunken == Stats.status.normal)
            myStats.isDrunk = false;
        else if (myStats.drunken == Stats.status.zero)
        {
            myStats.isDrunk = false;
        }

        if (Input.GetKeyDown("f"))
            StartCoroutine(StatOverTime("health",20,5));

        if (Input.GetKeyDown("g"))
            StartCoroutine(StatOverTime("hunger", -30, 10));

	}

    void ResetStats()
    {

        myStats.healthLVL = myStats.startHealth;
        myStats.hungerLVL = myStats.startHunger;
        myStats.happinessLVL = myStats.startHappiness;
        myStats.annoyanceLVL = myStats.startAnnoyance;
        myStats.energyLVL = myStats.startEnergy;
        myStats.drunkenLVL = myStats.startDrunken;

        myStats.isDead = false;
        myStats.isKO = false;
        myStats.isAsleep = false;
        myStats.isAngry = false;
    }

    IEnumerator StatOverTime(string statType, int statVal, float duration)
	{
		//float interval = 1;
		float tick = statVal / duration;

        //interval -= Time.deltaTime;

        Debug.Log("Called StatOverTime with " + statType + ", value:" + statVal + ", for a duration of " + duration + " seconds.");

		while (duration > 0) {
            //interval -= Time.deltaTime;

            Debug.Log("Duration is " + duration);
            //Debug.Log("Interval is " + interval);

			//if (interval <= 0) {
                Debug.Log("choosing stat");
                switch (statType)
                {
                    case "health":
                        myStats.healthLVL += tick;
                        break;
                    case "hunger":
                        myStats.hungerLVL += tick;
                        break;
                    case "happiness":
                        myStats.happinessLVL += tick;
                        break;
                    case "annoyance":
                        myStats.annoyanceLVL += tick;
                        break;
                    case "energy":
                        myStats.energyLVL += tick;
                        break;
                    case "drunken":
                        myStats.drunkenLVL += tick;
                        break;
                    default:
                        Debug.Log("Error: StatOverTime type: " + statType + "not known.");
                        break;
                }
				duration -= 1;
                //interval = 1;
			//}
            yield return new WaitForSeconds(1);
		}
	}
}
