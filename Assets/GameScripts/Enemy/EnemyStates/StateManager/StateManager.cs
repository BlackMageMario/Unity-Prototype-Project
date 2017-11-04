﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public State currentState;
	public State remainState;
    //public WeaponBehaviour weapon;
    public GameObject target;
    public EnemyMovementStats movementStats;
    [HideInInspector]public Rigidbody body;
    // Use this for initialization
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
		target = GameObject.Find("PlayerPrototype");
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        currentState.UpdateState(this);
        //let's just go in a straight line for now
    }
    // Update is called once per frame
    /*void Update () {
        //maybe have an aiActive variable to check if AI is active
        currentState.UpdateState(this);
	}*/
	public void TransitionToState(State nextState)
	{
		if (nextState != remainState)
		{
			currentState = nextState;
		}
	}
}