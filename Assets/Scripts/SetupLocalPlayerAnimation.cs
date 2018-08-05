using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayerAnimation : NetworkBehaviour {
    Animator animator;

    [SyncVar(hook = "OnChangeAnimation")]
    public string animState = "idle";

    void OnChangeAnimation (string aS)
    {
        if (isLocalPlayer)
        {
            return;
        }
        UpdateAnimationState(aS);
    }

    [Command]
    public void CmdChangeAnimState(string aS)
    {
        UpdateAnimationState(aS);
    }

    void UpdateAnimationState(string aS)
    {
        if (animState == aS) {
            return;
        }
        animState = aS;
        if (animState == "idle")
        {
            animator.SetBool("Idling", true);
        }
        else if (animState == "run")
        {
            animator.SetBool("Idling", false);
        }
        else if (animState == "attack")
        {
            animator.SetTrigger("Attacking");
        }
    }

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Idling", true);

        if (isLocalPlayer)
        {
            GetComponent<MyPlayerController>().enabled = true;
            CameraFollow360Player.player = this.gameObject.transform;
        }
        else 
        {
            GetComponent<MyPlayerController>().enabled = false;
        }
	}
}
