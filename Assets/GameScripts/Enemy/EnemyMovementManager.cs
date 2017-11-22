using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages the movement of an enemy
/// </summary>
public class EnemyMovementManager : MonoBehaviour {
    public EnemyMovementStats movementStats;
    public GameObject playerTarget;
    protected Rigidbody body;
	protected virtual void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        //let's just go in a straight line for now
        Vector3 direction = (playerTarget.transform.position - this.transform.position).normalized;
        transform.LookAt(playerTarget.transform.position);
        body.AddForce(direction * movementStats.forwardSpeed);//this might not work for properly applying a force?

	}
}
