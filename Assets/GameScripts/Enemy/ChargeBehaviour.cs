using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A basic script that allows the enemy it is attached to to charge.
/// </summary>
public class ChargeBehaviour : MonoBehaviour {
    public float chargeRange;//the range at which a charge can be attempted
    public float chargeForce;//force of the charge
    public float jumpForce;//force of jump before charge
    public float coolDown;//how long the cooldown is for the charge
    private bool chargeOffCooldown;//checks whether we can charge right now
	void Start () {
        chargeOffCooldown = true;//set this to true otherwise we'd never be able to charge
	}
	
	public void DoChargeAttack()
    {
        Rigidbody body = GetComponent<Rigidbody>();//get our rigidbody
        if (chargeOffCooldown)
        {
            //look at the target
            transform.LookAt(GetComponent<StateManager>().target.transform.position);
            body.velocity = Vector3.zero;//set our motion to zero before charging
            body.angularVelocity = Vector3.zero;
            body.AddForce(body.transform.forward * chargeForce);
            body.AddForce(body.transform.up * jumpForce);
            StartCoroutine(putChargeOnCooldown());//put our charge on cooldown
        }
    }
    public bool isChargeFinished()
    {
        return GetComponent<Rigidbody>().velocity.magnitude <= 0;//the charge is finished when we stop moving
    }
    public bool CanChargeTarget()
    {
        return (Vector3.Distance(GetComponent<StateManager>().target.transform.position, transform.position) <= chargeRange && chargeOffCooldown);
    }
	void Update () {
		
	}
    IEnumerator putChargeOnCooldown()
    {
        chargeOffCooldown = false;
        while (!(GetComponent<Rigidbody>().velocity.magnitude <= 0))//while the charge isn't finished
        {
            yield return new WaitForFixedUpdate();
        }
        //when it is, yield again for the cooldown time
        yield return new WaitForSeconds(coolDown);
        chargeOffCooldown = true;
    }
}
