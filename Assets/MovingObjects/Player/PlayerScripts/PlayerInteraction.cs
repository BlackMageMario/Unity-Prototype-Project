using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	private Camera bodyCamera;
	void Start () {
		bodyCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
}
