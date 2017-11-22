using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
/// <summary>
/// Chase our target if we are not in the engage distance
/// </summary>
public class ChaseAction : Action
{
    public override void Act(StateManager manager)
    {
        Chase(manager);
    }
    private void Chase(StateManager manager)
    {
        //check our position and see if it is greater than the engage distance
        if (Vector3.Distance(manager.transform.position, manager.target.transform.position) > manager.movementStats.engageDistance)
        {
            //if it is, look at the target and run in its direction
            Vector3 direction = (manager.target.transform.position - manager.transform.position).normalized;
            manager.transform.LookAt(manager.target.transform.position);
            manager.body.AddForce(direction * manager.movementStats.forwardSpeed);
        }    
    }
}
