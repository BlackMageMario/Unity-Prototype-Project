using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// State managers for enemies
/// Modified and adapted from Pluggable AI tutorial from Unity - https://unity3d.com/learn/live-training/session/pluggable-ai-scriptable-objects
/// </summary>
public class StateManager : MonoBehaviour {
    public State currentState;//the current state of the enemy - also our initial state as set in the inspecter
	public State remainState;//the remain in state - default state that we check our decisions against
    public GameObject target;//the target we chase after - i.e. the player
    public EnemyMovementStats movementStats;//our movement stats for enemies
    [HideInInspector]public Rigidbody body;//the rigidbody for the enemy
    // Use this for initialization
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
		target = GameObject.Find("PlayerPrototype");//simplest way of finding the player object - we could have also used a singleton set by the player
        //script that all objects could access
    }
    //we only want the State Machine to execute on every fixed update
    protected virtual void FixedUpdate()
    {
		GameState state = GameStateManager.instance.GetCurrentGameState();
		if(state != GameState.DEAD)//if the player is DEAD we don't update the state
		{
			currentState.UpdateState(this);
		}
    }
	public void TransitionToState(State nextState)
	{
        //we only change the state if it is not equal to the remain in state
		if (nextState != remainState)
		{
			currentState = nextState;
		}
	}
}
