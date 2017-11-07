using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUp {

    // Use this for initialization
    public int healthAmount;//probably could use health stats
    // Update is called once per frame
    protected override void executeAction(GameObject execute)
    {
        execute.GetComponent<HealthManager>().increaseHealth(healthAmount);
    }
}
