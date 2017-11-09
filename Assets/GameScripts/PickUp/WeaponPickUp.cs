﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : PickUp
{
    public WeaponBehaviour WeaponToGive;
    protected override void executeAction(GameObject execute)
    {
        if(!(execute.GetComponent<WeaponManager>().addWeapon(WeaponToGive)))
        {
			//failed to add weapon 
			Debug.Log("Failed to add weapon");
            //what could we do? We haven't implemented proper ammo system
        }
		else
		{
			Destroy(GetComponent<Rigidbody>());//refactor
			Destroy(GetComponent<SphereCollider>());//refactor
			Destroy(this);
		}
    }
}