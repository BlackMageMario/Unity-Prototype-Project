﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float yCameraMovement;
    private float xCameraMovement;//not used for now
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        xCameraMovement += 2 * Input.GetAxis("Mouse X");
        //float xCameraMovement = Input.GetAxis("Mouse X");
        yCameraMovement -= 2* Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(Mathf.Clamp(yCameraMovement, -90, 90), xCameraMovement, 0);
    }
}
