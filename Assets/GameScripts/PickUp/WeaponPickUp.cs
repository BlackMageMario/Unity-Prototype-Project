using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : PickUp
{
    public WeaponBehaviour WeaponToGive;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerPrototype")
        {
            UIManager.instance.generateWeaponPopUp(gameObject.name);//maybe we should put the name of the weapon here somewhere
        }
    }
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
            Collider[] colliders = GetComponents<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
            }
            UIManager.instance.clearWeaponPopUp();
			Destroy(this);
		}
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerPrototype")
        {
            UIManager.instance.clearWeaponPopUp();//just clear it
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.name == "PlayerPrototype")
        {
            if (Input.GetKey(KeyCode.E))
            {
                executeAction(other.gameObject);
            }

        }
    }
}
