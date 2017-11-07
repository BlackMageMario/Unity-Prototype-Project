using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
public class ChaseAction : Action
{
    public override void Act(StateManager manager)
    {
        Chase(manager);
    }
    private void Chase(StateManager manager)
    {
        if (Vector3.Distance(manager.transform.position, manager.target.transform.position) > manager.movementStats.engageDistance)
        {
            Vector3 direction = (manager.target.transform.position - manager.transform.position).normalized;
            manager.transform.LookAt(manager.target.transform.position);
            manager.body.AddForce(direction * manager.movementStats.forwardSpeed);
        }    
    }
}
