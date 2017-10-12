using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
public class ShootAction : Action {

    public override void Act(StateManager manager)
    {
        Shoot(manager);
    }
    private void Shoot(StateManager manager)
    {
        if (Vector3.Distance(manager.transform.position, manager.target.transform.position) < manager.movementStats.engageDistance)
        {
            manager.transform.LookAt(manager.target.transform.position);
            manager.weapon.fireGun();
        }
    }

}
