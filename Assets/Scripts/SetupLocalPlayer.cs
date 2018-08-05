using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class SetupLocalPlayer : NetworkBehaviour {
    public Text namePrefab;
    public Text nameLabel;
    public Text timerPrefab;
    public Text timerLabel;
    public Transform timerPos;
    private float startTime;
    public Transform namePos;
    string textboxname = "";
    string colorboxname = "";
    public Slider healthPrefab;
    public Slider health;
    public GameObject explosion;
    NetworkStartPosition[] spawnPos;

    [SyncVar(hook = "OnChangeTeam")]
    public int teamNumber = 1;

    // hooks mean that when the syncvar value changes, the hook function will run!
    [SyncVar (hook = "OnChangeName")]
    public string pName = "player";

    [SyncVar (hook = "OnChangeColor")]
    public string pColor = "#ffffff";

    [SyncVar(hook = "OnChangeHealth")]
    public int healthValue = 100;



    // Run this when your client starts, add code here to sync information for new joiners
    public override void OnStartClient()
    {
        base.OnStartClient();
        // Run UpdateStates after a 1 sec delay
        Invoke("UpdateStates", 1);
    }

    void UpdateStates()
    {
        OnChangeName(pName);
        OnChangeColor(pColor);
    }

    void OnChangeName(string n)
    {
        pName = n;
        nameLabel.text = pName;
    }

    void OnChangeColor(string n)
    {
        ChangeColor(n);
    }

    void OnChangeHealth(int n)
    {
        healthValue = n;
        health.value = healthValue;
    }

    void OnChangeTeam(int n)
    {
        teamNumber = n;
        // Change the players character here.
        if (n == 2)
        {
            // Spawn as banana
        }
        else
        {
            // Spawn as monkey
        }
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (spawnPos != null && spawnPos.Length > 0)
        {
            this.transform.position = spawnPos[Random.Range(0, spawnPos.Length)].transform.position;
        }
    }

    // Run this function on the server
    [Command]
    public void CmdChangeName(string newName)
    {
        pName = newName;
        nameLabel.text = pName;
    }

    [Command]
    public void CmdChangeColor(string newColor)
    {
        ChangeColor(newColor);
    }

    [Command]
    public void CmdChangeHealth(int hitValue)
    {
        healthValue = healthValue + hitValue;
        health.value = healthValue;

        if (health.value <= 0)
        {
            GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            NetworkServer.Spawn(e);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            RpcRespawn();
            healthValue = 100;
        }
    }

    //[Command]
    //public void CmdUpdatePlayerCharacter(int cid)
    //{
    //    NetworkManager.singleton.GetComponent<CustomNetworkManager>().SwitchPlayer(this, cid);
    //}

    void ChangeColor(string newColor)
    {
        pColor = newColor;
        Renderer []
        rends = GetComponentsInChildren<Renderer>();

        foreach(Renderer r in rends )
        {
            if(r.gameObject.name == "Monkey")
            {
                r.material.SetColor("_Color", ColorFromHex(pColor));
            }
        }
    }

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (Event.current.Equals(Event.KeyboardEvent("0")) ||
                Event.current.Equals(Event.KeyboardEvent("1")) ||
                Event.current.Equals(Event.KeyboardEvent("2")) ||
                Event.current.Equals(Event.KeyboardEvent("3")))
            {
                int charid = int.Parse(Event.current.keyCode.ToString().Substring(5)) + 1;
                //CmdUpdatePlayerCharacter(charid);
            }

            textboxname = GUI.TextField(new Rect(25, 15, 100, 25), textboxname);
            if(GUI.Button(new Rect(130, 15, 35, 25), "Set"))
            {
                CmdChangeName(textboxname);
            }

            colorboxname = GUI.TextField(new Rect(170, 15, 100, 25), colorboxname);
            if(GUI.Button(new Rect(275, 15, 35, 25), "Set"))
            {
                CmdChangeColor(colorboxname);
            }
        }
    }

    Color ColorFromHex(string hex)
    {
        hex = hex.Replace("0x", "");
        hex = hex.Replace("#", "");
        byte a = 255;
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        if (hex.Length == 0)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }

        return new Color32(r, g, b, a);
    }


    // Awake happens before start
    void Awake()
    {
        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLabel.transform.SetParent(canvas.transform);
    }

    // Use this for initialization
    void Start () {

        startTime = 30.0f;

        if (isLocalPlayer)
        {
            GetComponent<MyPlayerController>().enabled = true;
            CameraFollow360Player.player = this.gameObject.transform;
            GameObject canvas = GameObject.FindWithTag("MainCanvas");
            timerLabel = Instantiate(timerPrefab, Vector3.zero, Quaternion.identity) as Text;
            timerLabel.transform.SetParent(canvas.transform);
        }
        else
        {
            GetComponent<MyPlayerController>().enabled = false;
        }
        //health = Instantiate(healthPrefab, Vector3.zero, Quaternion.identity) as Slider;
        //health.transform.SetParent(canvas.transform);


        spawnPos = FindObjectsOfType<NetworkStartPosition>();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (isLocalPlayer && collision.gameObject.tag == "bullet")
        {
            CmdChangeHealth(-5);
        }
    }

    // Update is called once per frame
    void Update () {
        //determine if the object is inside the camera's viewing volume
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(this.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 &&
                   screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        //if it is on screen draw its label attached to is name position
        if (onScreen)
        {
            Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
            nameLabel.transform.position = nameLabelPos;

            Vector3 timerLabelPos = Camera.main.WorldToScreenPoint(timerPos.position);
            timerLabel.transform.position = timerLabelPos;
            //health.transform.position = nameLabelPos + new Vector3(0, 15, 0);
        }
        else
        {
            //otherwise draw it WAY off the screen 
            nameLabel.transform.position = new Vector3(-1000, -1000, 0);
            //health.transform.position = new Vector3(-1000, -1000, 0);
        }

	}

    public void OnDestroy()
    {
        // If character is destroyed, destroy it's name as well
        if(nameLabel != null && health != null)
        {
            Destroy(nameLabel.gameObject);
            Destroy(health.gameObject);
        }
    }
}
