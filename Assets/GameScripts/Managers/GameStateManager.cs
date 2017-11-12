using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Singleton pattern found from: https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
/// Example C# code used to design state machine: https://stackoverflow.com/questions/5923767/simple-state-machine-example-in-c
/// </summary>
public class GameStateManager : MonoBehaviour {

	// Use this for initialization
	Dictionary<StateTransition, GameState> transitions;
	public static GameStateManager instance = null;
	private GameState currentState;
	private GameState initialState = GameState.STARTMENU;
	private GameObject player;
	private Camera ourCamera;
	void Awake()
	{
		if (!instance)//if this doesn't exist
		{
            DontDestroyOnLoad(this.gameObject);
            instance = this;//this is our singleton
		}
		else
		{
			Destroy(this.gameObject);//destroy; extra singleton created
		}
        createTransitions();
	}
	private void createTransitions()
	{
		currentState = initialState;
		transitions = new Dictionary<StateTransition, GameState>
        {
			//format: new StateTraansition(startState, theTransition) and stateToTransitionTo
			{new StateTransition(GameState.STARTMENU, TransitionEnum.BEGINGAME), GameState.GAMEALIVE },
            {new StateTransition(GameState.STARTMENU,  TransitionEnum.GAMEMENUEXIT), GameState.EXIT },
			{new StateTransition(GameState.GAMEALIVE, TransitionEnum.PAUSEGAME), GameState.GAMEPAUSE },
			{new StateTransition(GameState.GAMEALIVE, TransitionEnum.GAMEDIE), GameState.DEAD },
			{new StateTransition(GameState.GAMEPAUSE, TransitionEnum.UNPAUSEGAME), GameState.GAMEALIVE },
			{new StateTransition(GameState.GAMEPAUSE, TransitionEnum.PAUSEEXIT), GameState.EXIT},
			{new StateTransition(GameState.DEAD, TransitionEnum.DEADEXIT), GameState.EXIT},
			{new StateTransition(GameState.DEAD, TransitionEnum.RESTARTLIFE), GameState.GAMEALIVE}
		};
	}
	private GameState NextState(TransitionEnum transition)
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
	private GameState MoveNext(TransitionEnum transition)
	{
		return NextState(transition);
    }
	public GameState GetCurrentGameState()
	{
		return currentState;
	}
    public void QuitGame()
    {
		
        if(MoveNext(TransitionEnum.GAMEMENUEXIT) != currentState 
            || MoveNext(TransitionEnum.PAUSEEXIT) != currentState 
                || MoveNext(TransitionEnum.DEADEXIT) != currentState)//check if the tranisition is valid
        {
			//... then just quit
			//like. the application is closing. you *could* like. check the state.
			//... why would you want to :U
			//Debug.Log("Current state: " + currentState);
			Application.Quit();
        } 
    }
    public void PauseGame()
    {
        GameState potentialState = MoveNext(TransitionEnum.PAUSEGAME);
        if (potentialState != currentState)
        {
            //can make the transition
            currentState = potentialState;
            UIManager.instance.GameRunningUI.gameObject.SetActive(false);
            UIManager.instance.GamePauseUI.gameObject.SetActive(true);
			Time.timeScale = 0;
            //what do we do waith the pause state?

            //well we get the pause menu and bring that up
            //the real question is if we let all the other objects like. know to pause. or not.
            //.... this is the trickest state by far
        }
    }
    public void PlayerDead()
    {
        GameState potentialState = MoveNext(TransitionEnum.GAMEDIE);
        if(potentialState != currentState)
        {
            currentState = potentialState;
            UIManager.instance.GameRunningUI.gameObject.SetActive(false);
            UIManager.instance.GameDeadUI.gameObject.SetActive(true);
			player = GameObject.Find("PlayerPrototype");
			ourCamera = GameObject.FindObjectOfType<Camera>();
			//move the camera to some point
            //bring up the "you're dead" menu
            //like pause, inform everything that the player is dead. Shut off the wave manager
        }
    }

	public void RestartLife()
	{
		GameState potentialState = MoveNext(TransitionEnum.RESTARTLIFE);
		if (potentialState != currentState)
		{
			currentState = potentialState;
			UIManager.instance.GameDeadUI.gameObject.SetActive(false);
			UIManager.instance.GameRunningUI.gameObject.SetActive(true);
			if (player)
			{
				player.GetComponent<HealthManager>().resetHealth();
				//reattach camera

			}
			//restart the game
			//call reset on *basically* everything
		}
	}

	public void UnPauseGame()
    {
        GameState potentialState = MoveNext(TransitionEnum.UNPAUSEGAME);
        if(potentialState != currentState)
        {
            currentState = potentialState;//find a better way of doing this...
            UIManager.instance.GamePauseUI.gameObject.SetActive(false);
            UIManager.instance.GameRunningUI.gameObject.SetActive(true);
			Time.timeScale = 1;
            //my brian is probably having trouble with logic really
            //do the opposite of the pause state, take down the pause menu and replace it
            //with the main UI state
        }
    }

    public void BeginGame()
    {
        GameState potentialState = MoveNext(TransitionEnum.BEGINGAME);
        if(potentialState != currentState)
        {
            currentState = potentialState;
            UIManager.instance.GameStartUI.gameObject.SetActive(false);
			SceneManager.LoadScene("TestScene");
            UIManager.instance.GameRunningUI.gameObject.SetActive(true);
            //begin the game
            //this might be a simple scene change... perhaps the main menu should be its own scene
            //perhaps make the gamestatemanager a DontDestroyOnLoad() object
        }
    }

 
}
class StateTransition
{
    readonly GameState CurrentState;
    readonly TransitionEnum Transition;
    public StateTransition(GameState currentState, TransitionEnum transition)
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
public enum GameState
{
	STARTMENU,
	GAMEALIVE,
	GAMEPAUSE,
	DEAD,
	EXIT
};

public enum TransitionEnum
{
	BEGINGAME,
	PAUSEGAME,
	GAMEDIE,
	PAUSEEXIT,
	DEADEXIT,
	UNPAUSEGAME,
	RESTARTLIFE,
	GAMEMENUEXIT,
	PAUSEMENUEXIT
};
