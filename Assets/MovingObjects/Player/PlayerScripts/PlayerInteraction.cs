using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	private Camera bodyCamera;
	void Start () {
		bodyCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //weapon manage will handle firing
        //and picking up weapon
		//bodyCamera.
	}
}
