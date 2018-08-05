using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Attack : NetworkBehaviour {
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            CmdTag();
        }
    }

    void Tag()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "seeker")
            {
                Destroy(hit.transform.gameObject);
            }
            else 
            {
                Destroy(this.gameObject);    
            }
        }
    }


    [Command]
    void CmdTag()
    {
        Tag();

    }
}
