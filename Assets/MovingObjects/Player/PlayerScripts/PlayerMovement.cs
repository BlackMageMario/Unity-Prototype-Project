using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float forwardSpeed;
	public float backwardSpeed;
	public float sidewaySpeed;
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
            body.AddForce(transform.forward * forwardSpeed);
            //this one is not time.deltatimed'd
            //body.MovePosition(this.transform.position + new Vector3(0, 0, 1) * Time.deltaTime * 20);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(transform.forward * -1 * backwardSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * sidewaySpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(transform.right * -1 * sidewaySpeed);
        }
		if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1f))
		{
			body.AddForce(transform.up * 400);
		}
        transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, bodyCamera.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
