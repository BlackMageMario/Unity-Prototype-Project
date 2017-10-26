using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Example C# code used to design state machine: https://stackoverflow.com/questions/5923767/simple-state-machine-example-in-c
/// </summary>
public class GameStateManager : MonoBehaviour {

	// Use this for initialization
	//how do we want to build this?
	//four states:
	// 1. Menu 2. Alive, Playing 3. Pause 4. Dead, Restart
	// go from menu-> Alive -> Pause, Alive->Dead, Dead-> Alive
	
	class StateTransition
	{
		readonly GameState CurrentState;
		readonly Transition Transition;
		public StateTransition(GameState currentState, Transition transition)
		{
			CurrentState = currentState;
			Transition = transition;
		}
		public override int GetHashCode()
		{
			return 17 + 31 * CurrentState.GetHashCode() + 31 * Transition.GetHashCode();//review hash codes to understand what's going on here
		}
		public override bool Equals(object obj)
		{
			StateTransition other = obj as StateTransition;
			return other != null && this.CurrentState == other.CurrentState
				&& this.Transition == other.Transition;
		}
	}
	Dictionary<StateTransition, GameState> transitions;
	public static GameStateManager instance = null;
	private GameState currentState;
	private GameState initialState = GameState.GAMEALIVE;
	void Awake()
	{
		if (!instance)//if this doesn't exist
		{
			instance = this;//this is our singleton
		}
		else
		{
			Destroy(this.gameObject);//destroy; extra singleton created
		}
		currentState = initialState;
	}
	private void createTransitions()
	{
		currentState = initialState;
		transitions = new Dictionary<StateTransition, GameState>
		{
			//format: new StateTraansition(startState, theTransition) and stateToTransitionTo
			{new StateTransition(GameState.STARTMENU, Transition.BEGINGAME), GameState.GAMEALIVE },
			{new StateTransition(GameState.GAMEALIVE, Transition.PAUSEGAME), GameState.GAMEPAUSE },
			{new StateTransition(GameState.GAMEALIVE, Transition.GAMEDIE), GameState.DEAD },
			{new StateTransition(GameState.GAMEPAUSE, Transition.UNPAUSEGAME), GameState.GAMEALIVE },
			{new StateTransition(GameState.GAMEPAUSE, Transition.PAUSEEXIT), GameState.EXIT},
			{new StateTransition(GameState.DEAD, Transition.DEADEXIT), GameState.EXIT},
			{new StateTransition(GameState.DEAD, Transition.RESTARTLIFE), GameState.GAMEALIVE},
			{new StateTransition(GameState.EXIT, Transition.EXITTOMENU), GameState.STARTMENU}
		};
	}
	private GameState NextState(Transition transition)
	{
		StateTransition attemptTransition = new StateTransition(currentState, transition);
		GameState nextState;
		if (!transitions.TryGetValue(attemptTransition, out nextState))//if this line works, out is set as the state we want
		{
			//can't get a valid value
			//we could throw exception or what i would prefer: a warning
			Debug.LogError("Invalid transition: " + currentState + " -> " + transition);
			//keep state as current state
			return currentState;
		}
		else
		{
			return nextState;
		}
	}
	public GameState MoveNext(Transition transition)
	{
		currentState = NextState(transition);
		return currentState;
	}
	public GameState GetCurrentGameState()
	{
		return currentState;
	}
	/*
	public void ChangeState(GameState state)
	{
		if(currentState == GameState.GAMEALIVE && (state == GameState.GAMEPAUSE || state == GameState.DEAD))
		{
			//we can change the state to that
			currentState = state;
		}
		if(currentState == GameState.STARTMENU && (state == GameState.GAMEALIVE))
		{
			currentState = state;
		}
		if(currentState == GameState.GAMEPAUSE && (state == GameState.GAMEALIVE))
		{
			currentState = state;
		}
	}
	*/
	//void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}

public enum GameState
{
	STARTMENU,
	GAMEALIVE,
	GAMEPAUSE,
	DEAD,
	EXIT
};

public enum Transition
{
	BEGINGAME,
	PAUSEGAME,
	GAMEDIE,
	PAUSEEXIT,
	DEADEXIT,
	UNPAUSEGAME,
	RESTARTLIFE,
	EXITTOMENU
};
