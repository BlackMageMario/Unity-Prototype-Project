using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehaviour : MonoBehaviour {
    public float chargeRange;
    public float chargeForce;//force of the charge
    public float jumpForce;//force of jump before charge
    public float coolDown;//how long the cooldown is for the charge
    private bool chargeOffCooldown;//can we charge right now
	void Start () {
        chargeOffCooldown = true;
	}
	
	public void DoChargeAttack()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (chargeOffCooldown)
        {
            transform.LookAt(GetComponent<StateManager>().target.transform.position);
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.AddForce(body.transform.forward * chargeForce);
            body.AddForce(body.transform.up * jumpForce);
            StartCoroutine(putChargeOnCooldown());
        }
    }
    public bool isChargeFinished()
    {
        return GetComponent<Rigidbody>().velocity.magnitude <= 0;
    }
    public bool CanChargeTarget()
    {
        return (Vector3.Distance(GetComponent<StateManager>().target.transform.position, transform.position) <= chargeRange && chargeOffCooldown);
    }
	void Update () {
		
	}
    IEnumerator putChargeOnCooldown()
    {
        Debug.Log("Putting charge on cooldown");
        chargeOffCooldown = false;
        while (!(GetComponent<Rigidbody>().velocity.magnitude <= 0))
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(coolDown);
        chargeOffCooldown = true;
    }
}
