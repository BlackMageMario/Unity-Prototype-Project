using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public WeaponBehaviour startingWeapon;//weapon we start with
    [Range(1, 10)]public int maxNumWeapon;//max number of weapons we can have
    private Dictionary<KeyCode, WeaponBehaviour> weaponsInInventory;
    private Dictionary<int, KeyCode> numberKeys;
    private WeaponBehaviour currentWeapon;//let's refactor this
    private bool canAttack;
    private Camera weaponCamera;
	// Use this for initialization
	void Start () {
        weaponsInInventory = new Dictionary<KeyCode, WeaponBehaviour>(maxNumWeapon);
        if (startingWeapon)
        {
            addWeapon(startingWeapon);
            currentWeapon = startingWeapon;
        }
        //this is bad but not as bad as the old implementation
        numberKeys = new Dictionary<int, KeyCode>();
        numberKeys.Add(0, KeyCode.Alpha0);
        numberKeys.Add(1, KeyCode.Alpha1);
        numberKeys.Add(2, KeyCode.Alpha2);
        numberKeys.Add(3, KeyCode.Alpha3);
        numberKeys.Add(4, KeyCode.Alpha4);
        numberKeys.Add(5, KeyCode.Alpha5);
        numberKeys.Add(6, KeyCode.Alpha6);
        numberKeys.Add(7, KeyCode.Alpha7);
        numberKeys.Add(8, KeyCode.Alpha8);
        numberKeys.Add(9, KeyCode.Alpha9);
        weaponCamera = GetComponentInChildren<Camera>();
        canAttack = true;
    }

    void Update()//this could be update too?? - changed to update
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
    public bool addWeapon(WeaponBehaviour weapon)
    {
        if(weaponsInInventory.Count < maxNumWeapon)
        {
            //we can add a weapon
            //now check if weapon isn't already in inventory
            if(!weaponsInInventory.ContainsValue(weapon))
            {
                KeyCode keyWeWant = KeyCode.A;
                int numberKey = (weaponsInInventory.Count + 1) % 10;
                //we could put a try catch on this code
                //but this should never fail
                // - my last words, 2017
                numberKeys.TryGetValue(numberKey, out keyWeWant);
                Debug.Log("The key: " + keyWeWant);
                weaponsInInventory.Add(keyWeWant, weapon);
                return true;
            }
        }
        return false;
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
