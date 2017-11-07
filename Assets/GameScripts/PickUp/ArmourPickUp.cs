using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickUp : PickUp {
    public int armourAmount;
    protected override void executeAction(GameObject execute)
    {
        //base.executeAction(execute);
        execute.GetComponent<HealthManager>().increaseArmour(armourAmount);
    }
}
