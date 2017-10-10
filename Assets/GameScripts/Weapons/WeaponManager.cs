using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public WeaponBehaviour currentWeapon;
	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        if (currentWeapon.weaponData.singleFire)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentWeapon.fireGun();
            }
        }
        else
        {
            //automatic fire
            if (Input.GetKey(KeyCode.Mouse0))
            {
                currentWeapon.fireGun();
            }
        }
    }

    public void changeWeapon(WeaponBehaviour weapon)
    {
        currentWeapon = weapon;
    }
}
