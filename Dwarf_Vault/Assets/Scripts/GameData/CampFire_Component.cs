using UnityEngine;
using System.Collections;

public class CampFire_Component : MonoBehaviour {

    public float Timer;
    public float resetTimer = 15;
    public int PiecesRemainging;
    public bool LitFire;

    private Light fireLight;

    public bool toggle = false;

	void Start () 
    {
        Timer = resetTimer;
        PiecesRemainging = 1;
        LitFire = true;

        fireLight = GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            PiecesRemainging -= 1;
            if (PiecesRemainging <= 0)
            {
                PiecesRemainging = 0;
                LitFire = false;
                fireLight.enabled = false;
            }

            Timer = resetTimer;
        }

        if (toggle)
            LightFire(3);
	}

    public void LightFire(int fireWood)
    {
        LitFire = true;
        PiecesRemainging += fireWood;
        fireLight.enabled = true;

        toggle = false;
    }
}
