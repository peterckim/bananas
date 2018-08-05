using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour {
    static CharacterCustomizer CC;
    static public GameObject myCharacter;
    public Texture[] MonkeyTops;
    public Texture[] BananaTops;

    public void ChangeTopTexture(int i)
    {
        if (myCharacter.name.Contains("Monkey"))
        {
            myCharacter.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = MonkeyTops[i];
        }
        else if (myCharacter.name.Contains("Banana"))
        {
            myCharacter.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = BananaTops[i];
        }

        //myCharacter.GetComponent<SetupLocalPlayer>().CmdChangeTop(i);
    }

    static public Texture GetTop(int i, string name)
    {
        if (name.Contains("Monkey"))
        {
            return (CC.MonkeyTops[i]);
        }
        else if (name.Contains("Banana"))
        {
            return (CC.BananaTops[i]);
        }

        return null;
    }

	// Use this for initialization
	void Start () {
        CC = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
