using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    //from unity tutorials "Live Session: Pluggable AI with ScriptableObjects
    //this all uses the delegate pattern due to ScriptableObject holds values
    public Action[] actions;
    public void UpdateState(StateManager manager)
    {
        DoActions(manager);
    }
    private void DoActions(StateManager manager)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(manager);
        }

    }

}
