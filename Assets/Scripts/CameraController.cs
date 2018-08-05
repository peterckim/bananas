using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.parent.position = this.transform.position;
        Camera.main.transform.parent.rotation = this.transform.rotation;
	}
}
