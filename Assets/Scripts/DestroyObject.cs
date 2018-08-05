using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyObject : NetworkBehaviour {

    // We want to enlarge reticle when an object is not the localclient

    public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSeekerSingleClick(Touch touch)
    {
        Debug.Log("Click Working!");
        CmdTag();
    }

    [Command]
    public void CmdTag()
    {
        if (this.transform.name == "Banana")
        {
            GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            NetworkServer.Spawn(e);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(this);
        }
        else
        {
            //GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            //NetworkServer.Spawn(e);
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Destroy(this);
        }
    }

    //void Tag() {
    //    if (gameObject.tag == "hider")
    //    {
    //        Debug.Log(gameObject);
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log(gameObject);
    //        seeker = GameObject.FindWithTag("seeker");
    //        Destroy(seeker);
    //    }
    //}

    public void SetInGaze(bool isin) {
        if (isClient)
        {
            if (isin)
            {
                TouchListener.OnSingleClick += OnSeekerSingleClick;
            }
            else
            {
                TouchListener.OnSingleClick -= OnSeekerSingleClick;
            }
        }
    }
}
