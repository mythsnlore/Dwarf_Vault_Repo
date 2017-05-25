using UnityEngine;
using System.Collections;
using System.Collections.Generic; //made lists available

public class NameGen_Dwarf : MonoBehaviour 
{
    public List<string> GeneratedNames;

    public List<string> FirstFemale;
    public List<string> FirstMale;
    public List<string> LastA;
    public List<string> LastB;

    bool addingFemale;
    
	void Start () 
    {
        TextAsset nameText = Resources.Load<TextAsset>("DwarfFirst");

        string [] Lines = nameText.text.Split("\n"[0]); //searches for line breaks and splits the text up so we can read each name

        for (int i = 0; i < Lines.Length; i++) //look through list and find headers to add female or male names to different lists
        {
            if (Lines[i].Contains("Female:"))
            {
                addingFemale = true;
                Debug.Log("Adding Female Names");
                continue;
            }
            if (Lines[i].Contains("Male:"))
            {
                addingFemale = false;
                Debug.Log("Adding Male Names");
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

        TextAsset lastAText = Resources.Load<TextAsset>("DwarfLastA");
        string [] LastALines = lastAText.text.Split("\n"[0]);
        for (int i = 0; i < LastALines.Length; i++)
        {
            Debug.Log("Adding Last A");
            LastA.Add(LastALines[i]);
        }

        TextAsset lastBText = Resources.Load<TextAsset>("DwarfLastB");
        string [] LastBLines = lastBText.text.Split("\n"[0]);
        for (int i = 0; i < LastBLines.Length; i++)
        {
            Debug.Log("Adding Last B");
            LastB.Add(LastBLines[i]);
        }
	}

   string generateName ()
    {
        string maleName;
        string femaleName;
        string lastA;
        string lastB;

        lastA = LastA[Random.Range(0, LastA.Count)];
        lastB = LastB[Random.Range(0, LastB.Count)];

        if (Random.value >= 0.5f) //determine gender
        {
            //male = true;
            maleName = FirstMale[Random.Range(0, FirstMale.Count)];
            string FullmaleName = ("Male Dwarf " + maleName + " " + lastA + lastB);

            foreach (string name in GeneratedNames)
            {
                if (string.Equals(name, FullmaleName))
                {
                    if (FullmaleName.EndsWith(" II"))
                        FullmaleName = FullmaleName + "I";
                    else if (FullmaleName.EndsWith(" III") || FullmaleName.EndsWith(" IV"))
                    {
                        FullmaleName = FullmaleName.Remove(FullmaleName.Length - 2);
                        FullmaleName = FullmaleName + "V";
                    }
                    else
                    {
                        FullmaleName = FullmaleName + " II";
                        //name = name + ", first of thier name.";
                    }
                }
            }

            GeneratedNames.Add(FullmaleName);
            return FullmaleName;
        }
        else
        {
            //male = false;
            femaleName = FirstFemale[Random.Range(0, FirstFemale.Count)];
            string FullfemaleName = ("Female Dwarf "+ femaleName + " " + lastA + lastB);

            foreach (string name in GeneratedNames)
            {
                if (string.Equals(name, FullfemaleName))
                {
                    if (FullfemaleName.EndsWith(" II"))
                        FullfemaleName = FullfemaleName + "I";
                    else if (FullfemaleName.EndsWith(" III") || FullfemaleName.EndsWith(" IV"))
                    {
                        FullfemaleName = FullfemaleName.Remove(FullfemaleName.Length - 2);
                        FullfemaleName = FullfemaleName + "V";
                    }
                    else
                        FullfemaleName = FullfemaleName + " II";
                }
            }

            GeneratedNames.Add(FullfemaleName);
            return FullfemaleName;
        }
    }

    void OnGUI ()
    {
        if(GUI.Button(new Rect(50,50,100,50), "Gen Dwarf"))
            {
                Debug.Log (generateName());
            }
    }
}
