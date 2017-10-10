using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour 
{
    public float TimeStep = 5f;

    public bool TestEat;
    public bool TestHeal;

    //Hunger
    public float Hunger;
    public bool Hungry;
    public bool Starving;

    //Health
    public float Health;
    public bool Hurt;
    public bool BadlyHurt;
    public bool Dying;
    public bool Dead;


    void Update()
    {
        TimeStep -= Time.deltaTime;

        if (TimeStep <= 0)
        {
            Hunger += 1f;

            TimeStep = 5f;

            if (Starving)
            {
                Health -= 1f;
            }
        }

        if (Hunger <= 20)
        {
            Hungry = false;
        }
        else if (Hunger >= 50)
        {
            Hungry = true;
            if (Hunger > 100)
            {
                Starving = true;
            }
            else
                Starving = false;
        }
        else
        {
            //no change
        }

        if (TestEat)
        {
            Eat(10);
            TestEat = false;
        }

        if (TestHeal)
        {
            Heal(10);
            TestHeal = false;
        }

        //Hunger = Mathf.Clamp(Hunger, 0, 100);
    }

    public void Eat(float foodVal)
    {
        Hunger -= foodVal;
        Hunger = Mathf.Clamp(Hunger, 0, 99);
    }

    public void Heal(float healVal)
    {
        Health += healVal;
        Health = Mathf.Clamp(Health, 1, 100);
    }
}
