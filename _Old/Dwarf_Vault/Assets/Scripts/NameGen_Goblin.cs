using UnityEngine;
using System.Collections;
using System.Collections.Generic; //made lists available

public class NameGen_Goblin : MonoBehaviour 
{
	
    public List<string> GeneratedNames;

	public List<string> PrefixMale;
	public List<string> PrefixFemale;
    public List<string> FirstFemale;
    public List<string> FirstMale;
    public List<string> LastA;
	public List<string> LastB;

    bool addingFemale;
	bool title;
    
	void Start () 
    {
        TextAsset nameText = Resources.Load<TextAsset>("GoblinFirst");
        string [] Lines = nameText.text.Split("\n"[0]); //searches for line breaks and splits the text up so we can read each name
        for (int i = 0; i < Lines.Length; i++) //look through list and find headers to add female or male names to different lists
        {
            if (Lines[i].Contains("Female:"))
            {
                addingFemale = true;
                Debug.Log("Adding Female Goblin Names");
                continue;
            }
            if (Lines[i].Contains("Male:"))
            {
                addingFemale = false;
                Debug.Log("Adding Male Goblin Names");
                continue;
            }    

            if (Lines[i].Contains("---") != true)
            {
                if (addingFemale)
                {
                    FirstFemale.Add (Lines[i]);
                }
                else
                {
                    FirstMale.Add (Lines[i]);
                }
            }
        }

		TextAsset prefixText = Resources.Load<TextAsset> ("GoblinPrefix");
		string [] Pre = prefixText.text.Split("\n"[0]);
		for (int i = 0; i < Pre.Length; i++) //look through list and find headers to add female or male names to different lists
		{
			if (Pre[i].Contains("Female:"))
			{
				addingFemale = true;
                Debug.Log("Adding Female Goblin Prefixes");
				continue;
			}
			if (Pre[i].Contains("Male:"))
			{
				addingFemale = false;
                Debug.Log("Adding Male Goblin Prefixes");
				continue;
			}    

			if (Pre[i].Contains("---") != true)
			{
				if (addingFemale)
				{
					PrefixFemale.Add (Pre[i]);
				}
				else
				{
					PrefixMale.Add (Pre[i]);
				}
			}
		}

        TextAsset lastAText = Resources.Load<TextAsset>("GoblinLastA");
        string [] LastALines = lastAText.text.Split("\n"[0]);
        for (int i = 0; i < LastALines.Length; i++)
        {
            Debug.Log("Adding Last A Goblin");
            LastA.Add(LastALines[i]);
        }

        TextAsset lastBText = Resources.Load<TextAsset>("GoblinLastB");
        string [] LastBLines = lastBText.text.Split("\n"[0]);
        for (int i = 0; i < LastBLines.Length; i++)
        {
            Debug.Log("Adding Last B Goblin");
            LastB.Add(LastBLines[i]);
        }
	}

   string generateName ()
    {
		string malePre;
		string femalePre;
        string maleName;
        string femaleName;
        string lastA;
        //string lastB;

		int lastAIndex;

        if (Random.value >= 0.5f) //if male
        {
			malePre = PrefixMale[Random.Range(0, PrefixMale.Count)];
            maleName = FirstMale[Random.Range(0, FirstMale.Count)];
			string FullmaleName = ("Male Goblin " + malePre + maleName + " ");

			lastA = LastA[Random.Range(0, LastA.Count)];
            lastAIndex = LastA.IndexOf(lastA);

            if (lastAIndex == 0) //no monicker
            {
                //return FullmaleName;
            }
            else if (lastAIndex < 2) //the ever
            {
                FullmaleName = FullmaleName + lastA + " " + (LastA[Random.Range(4, LastA.Count)]);
                //return FullmaleName;
            }
            else if (lastAIndex == 2) //of the
            {
                FullmaleName = FullmaleName + lastA + " " + (LastA[Random.Range(4, LastA.Count)])+ " " + LastB[Random.Range(0, LastB.Count)];
                //return FullmaleName;
            }
            else //normal nickname
            {
                FullmaleName = FullmaleName + "the " + lastA;
                //return FullmaleName;
            }

            foreach (string name in GeneratedNames)
            {
                if (string.Equals(name, FullmaleName))
                {
                    Debug.Log("Match found");

                    if (FullmaleName.EndsWith(", son of " + malePre + maleName))
                    {
                        //just leave it
                    }
                    else 
                    {
                        FullmaleName = FullmaleName + ", son of " + malePre + maleName;
                    }
                }
            }
            GeneratedNames.Add(FullmaleName);
            return FullmaleName;
        }
        else //is female
        {
            femalePre = PrefixFemale[Random.Range(0, PrefixFemale.Count)];
            femaleName = FirstFemale[Random.Range(0, FirstFemale.Count)];

            string FullfemaleName = ("Female Goblin "+ femalePre + femaleName + " ");

            lastA = LastA[Random.Range(0, LastA.Count)];
            lastAIndex = LastA.IndexOf(lastA);

            if (lastAIndex == 0) // no monicker
            {
                //return FullfemaleName;
            }
            else if (lastAIndex < 2) //the ever
            {
                FullfemaleName = FullfemaleName + lastA + " " + (LastA[Random.Range(4, LastA.Count)]);
                //return FullfemaleName;
            }
            else if (lastAIndex == 2) //of the
            {
                FullfemaleName = FullfemaleName + lastA + " " + (LastA[Random.Range(4, LastA.Count)])+ " " + LastB[Random.Range(0, LastB.Count)];
                //return FullfemaleName;
            }
            else //normal nickname
            {
                FullfemaleName = FullfemaleName + "the " + lastA;
                //return FullfemaleName;
            }

            foreach (string name in GeneratedNames)
            {
                if (string.Equals(name, FullfemaleName))
                {
                    Debug.Log("Match found");

                    if (FullfemaleName.EndsWith(", daughter of " + femalePre + femaleName))
                    {
                        //just leave it
                    }
                    else 
                    {
                        FullfemaleName = FullfemaleName + ", daughter of " + femalePre + femaleName;
                    }
                }
            }
            GeneratedNames.Add(FullfemaleName);
            return FullfemaleName;
        }
    }

    void OnGUI ()
    {
        if(GUI.Button(new Rect(200,50,100,50), "Gen Goblin"))
            {
                Debug.Log (generateName());
            }
    }
}
