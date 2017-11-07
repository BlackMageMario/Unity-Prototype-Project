using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float forwardSpeed;
	public float backwardSpeed;
	public float sidewaySpeed;
    public float jumpForce;
    public float playerDashForce;
    public float dashCooldown;//cooldown for dashing
    private bool freezeMovement;
    private bool canDash;
    private Rigidbody body;
    private Camera bodyCamera;
	//we can dash whenever we want
    //and if your friends don't - SERIOUSLY? :u
	void Start () {
        body = GetComponent<Rigidbody>();
        bodyCamera = GetComponentInChildren<Camera>();
        canDash = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!freezeMovement)
        {
            KeyCode moveKey = KeyCode.Alpha0;
            bool moveKeyPressed = false;
            if (Input.GetKey(KeyCode.W))
            {
                //these below are the same and appear to be time.deltatime'd
                //body.velocity = new Vector3(0, 0, 1) * 20;
                //Debug.Log(transform.forward);
                body.AddForce(transform.forward * forwardSpeed);
                moveKey = KeyCode.W;
                moveKeyPressed = true;
                //this one is not time.deltatimed'd
                //body.MovePosition(this.transform.position + new Vector3(0, 0, 1) * Time.deltaTime * 20);
            }
            if (Input.GetKey(KeyCode.S))
            {
                body.AddForce(transform.forward * -1 * backwardSpeed);
                moveKey = KeyCode.S;
                moveKeyPressed = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(transform.right * sidewaySpeed);
                moveKey = KeyCode.D;
                moveKeyPressed = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(transform.right * -1 * sidewaySpeed);
                moveKey = KeyCode.A;
                moveKeyPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                Debug.Log("Jumping");
                body.AddForce(Vector3.up * jumpForce);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && moveKeyPressed && canDash)
            {
                body.AddForce(Vector3.up * jumpForce);//add half the jump force for a dash
                /*if(moveKey == KeyCode.W)
                {
                    body.AddForce(transform.forward * forwardSpeed * playerDashForce);
                }
                if(moveKey == KeyCode.S)
                {
                    body.AddForce(transform.forward * -1 * forwardSpeed * playerDashForce);
                }
                if (moveKey == KeyCode.D)
                {
                    body.AddForce(transform.right * sidewaySpeed * playerDashForce);
                }
                if (moveKey == KeyCode.A)
                {
                    body.AddForce(transform.right * -1 * sidewaySpeed * playerDashForce);
                }*/
                //i think getting the direction isn't working too bad - but see if we really want the normalized direction
                body.AddForce(body.velocity.normalized * forwardSpeed* playerDashForce);
                StartCoroutine(dashCooldownTimer());
                StartCoroutine(lockMovementDuringDash());
            }
        }
        transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, bodyCamera.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
    public void setFreeze(bool frozen)
    {
        freezeMovement = frozen;
    }
    IEnumerator dashCooldownTimer()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    IEnumerator lockMovementDuringDash()
    {
        //lock our movement during the dash
        //wait until our movement is zero (so we've stopped dashing) to allow us to move again
        //this should be a state machine but that would take longer to implement
        freezeMovement = true;
        while(freezeMovement)
        {
            if(GetComponent<Rigidbody>().velocity.y <= 0)
            {
                freezeMovement = false;
            }
            yield return new WaitForFixedUpdate();
        }
        freezeMovement = false;
    }
}
