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
		GameState state = GameStateManager.instance.GetCurrentGameState();
		if (state != GameState.DEAD)
		{
			if (!freezeMovement)
			{
				bool moveKeyPressed = false;
				if (Input.GetKey(KeyCode.W))
				{
					//these below are the same and appear to be time.deltatime'd
					//body.velocity = new Vector3(0, 0, 1) * 20;
					//Debug.Log(transform.forward);
					body.AddForce(transform.forward * forwardSpeed);
					moveKeyPressed = true;
					//this one is not time.deltatimed'd
					//body.MovePosition(this.transform.position + new Vector3(0, 0, 1) * Time.deltaTime * 20);
				}
				if (Input.GetKey(KeyCode.S))
				{
					body.AddForce(transform.forward * -1 * backwardSpeed);
					moveKeyPressed = true;
				}
				if (Input.GetKey(KeyCode.D))
				{
					body.AddForce(transform.right * sidewaySpeed);
					moveKeyPressed = true;
				}
				if (Input.GetKey(KeyCode.A))
				{
					body.AddForce(transform.right * -1 * sidewaySpeed);

					moveKeyPressed = true;
				}
				Debug.DrawRay(transform.position, Vector3.down, Color.red);
				if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 2f))
				{
					body.AddForce(Vector3.up * jumpForce);
				}
				if (Input.GetKeyDown(KeyCode.LeftShift) && moveKeyPressed && canDash)
				{
					body.AddForce(Vector3.up * jumpForce);//add half the jump force for a dash
					body.AddForce(body.velocity.normalized * forwardSpeed * playerDashForce);
					StartCoroutine(dashCooldownTimer());
					StartCoroutine(lockMovementDuringDash());
				}
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
