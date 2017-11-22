using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implementation of States for enemies
/// Modified and adapted from Pluggable AI tutorial from Unity - https://unity3d.com/learn/live-training/session/pluggable-ai-scriptable-objects
/// </summary>
[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    //this all uses the delegate pattern due to ScriptableObject holds values
    public Action[] actions;//the actions to execute
	public Transition[] transitions;//the transitions based on decisions
    public void UpdateState(StateManager manager)
    {
        //we only update the state when this is called
        //we could call it every fixed update, every update, through a coroutine, etc.
        DoActions(manager);
		CheckTransitions(manager);
    }
    private void DoActions(StateManager manager)
    {
        //loop through our array and do the actions
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(manager);
        }

    }
	private void CheckTransitions(StateManager manager)
	{
        //check our decisions
        //if our decision is true
        //we transition to the true state
		for (int i = 0; i < transitions.Length; i++)
		{
			bool decisionTrue = transitions[i].decision.Decide(manager);
			if(decisionTrue)
			{
				manager.TransitionToState(transitions[i].trueState);
			}
			else
			{
				manager.TransitionToState(transitions[i].falseState);
			}
		}
	}
}
