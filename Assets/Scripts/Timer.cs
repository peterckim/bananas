using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour {
    // Currently on the host's time, it is being updated, Not on clients.
    
    [SyncVar (hook = "OnChangeTimer")]
    double timerValue = 30.0f;

    private float startTime;
    public Text timer;


    void OnChangeTimer(double newTime)
    {
        timerValue = newTime;
        timer.text = "0:" + timerValue.ToString();
    }

	// Use this for initialization
	void Start () {
        startTime = 30.0f;
	}
	
	// Update is called once per frame
	void Update () {
        float t = startTime - Time.time;


        if (t <= 0.0f)
        {
            timerValue = 0.0f;
        }
        else
        {
            timerValue =  t;
        }

	}
}
