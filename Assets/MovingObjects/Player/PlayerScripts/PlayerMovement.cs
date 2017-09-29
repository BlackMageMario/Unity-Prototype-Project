using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody body;
    private Camera bodyCamera;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        bodyCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W))
        {
            //these below are the same and appear to be time.deltatime'd
            //body.velocity = new Vector3(0, 0, 1) * 20;
            body.AddForce(transform.forward * 20);
            //this one is not time.deltatimed'd
            //body.MovePosition(this.transform.position + new Vector3(0, 0, 1) * Time.deltaTime * 20);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(transform.forward * -1 * 20);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * 20);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(transform.right * -1 * 20);
        }
        transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, bodyCamera.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
