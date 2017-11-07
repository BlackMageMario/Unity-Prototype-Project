using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : PickUp
{
    public WeaponBehaviour WeaponToGive;
    protected override void executeAction(GameObject execute)
    {
        execute.GetComponent<WeaponManager>().changeWeapon(WeaponToGive);
    }
}
