using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public WeaponBehaviour currentWeapon;
    private bool canAttack;
    private Camera weaponCamera;
	// Use this for initialization
	void Start () {
        canAttack = true;
        weaponCamera = GetComponentInChildren<Camera>();
	}

    void FixedUpdate()
    {
        if(canAttack)
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
    }
    public void changeWeapon(WeaponBehaviour weapon)
    {
        currentWeapon = weapon;
    }
    public void setCanAttack(bool state)
    {
        canAttack = state;
    }
}
