using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// WeaponManager for player. This changes weapons, add weapons to the player's inventory,
/// decides to take input so we can attack, etc.
/// </summary>
public class WeaponManager : MonoBehaviour {
    public WeaponBehaviour startingWeapon;//weapon we start with
    [Range(1, 10)]public int maxNumWeapon;//max number of weapons we can have
    private Dictionary<KeyCode, WeaponBehaviour> weaponsInInventory;//what weapons we have in our inventory
    private Dictionary<int, KeyCode> numberKeys;//the number keys we want to select
    private WeaponBehaviour currentWeapon;//our current weapon
    private bool canAttack;//whether we can currently attack or not
    private Camera weaponCamera;//the camera of our weapon
	// Use this for initialization
	void Start () {
        //I apologise for this - I could not find a better method of organising weapon selection
        weaponsInInventory = new Dictionary<KeyCode, WeaponBehaviour>(maxNumWeapon);
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
        //if we have a starting weapon
		if (startingWeapon)
		{
			addWeapon(startingWeapon);
			currentWeapon = startingWeapon;
		}
		canAttack = true;
    }

    void Update()//this could be update too?? - changed to update
    {
        //this update listens for input from the player
        //and checks whether it will be used or ignored
		GameState state = GameStateManager.instance.GetCurrentGameState();
        //we should not allow the player to fire if the player is dead
        //or the game is paused
		if (state != GameState.DEAD && state != GameState.GAMEPAUSE)
		{
			if (canAttack)
			{
				if (currentWeapon.weaponData.singleFire)
				{
                    //single, semi-automatic fire
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
            //get our keylist
			List<KeyCode> keyList = new List<KeyCode>(weaponsInInventory.Keys);
			for (int i = 0; i < keyList.Count; i++)
			{
				if (Input.GetKey(keyList[i]))//if we press that button
				{
					WeaponBehaviour newWeapon;
                    //get the weapon we want
                    //(since we are using a list we need to use TryGetValue based on the key supplied
					weaponsInInventory.TryGetValue(keyList[i], out newWeapon);
					if (currentWeapon != newWeapon)
					{
						currentWeapon.gameObject.SetActive(false);//weapons are attached to the player object
                        //as an easy way to keep track of them
                        //therefore we need to disable the current one
                        //and set the new one active
						currentWeapon = newWeapon;
						newWeapon.gameObject.SetActive(true);

					}
				}
			}
		}//I also apologise for all the nested ifs
    }
	
    public bool addWeapon(WeaponBehaviour weapon)
    {
        //add a weapon if possible
        if(weaponsInInventory.Count < maxNumWeapon)
        {
            if(!weaponsInInventory.ContainsValue(weapon))
            {
                KeyCode keyWeWant = KeyCode.A;
                int numberKey = (weaponsInInventory.Count + 1) % 10;//simplest way to determine what key we want
                //we could put a try catch on this code
                //but this should never fail
                // - my last words, 2017
                numberKeys.TryGetValue(numberKey, out keyWeWant);//get our key
                weaponsInInventory.Add(keyWeWant, weapon);//allow our weapon to be able to be selected by that key
				weapon.transform.SetParent(weaponCamera.transform, false);
                //we set "false" here on set parent so that the scale of the object
                //will not be changed when it is attached to the parent
				weapon.gameObject.transform.localPosition = weapon.preferredPosition;
                //since our weapon is attached to the camera (so that the model moves
                //with where our camera rotates and moves, we have to
                //set it to the proper local position so that
                //it looks correct
                weapon.gameObject.transform.localScale = weapon.preferredScale;
                //change its scale so that it looks correct
                //when held by the player
				weapon.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                //give it a neutral rotation
				if(currentWeapon != null && currentWeapon.gameObject.activeSelf)
				{
                    //if we have a current weapon and it is active
                    //disable the attached weapon
					weapon.gameObject.SetActive(false);
				}
                return true;//return true if we have succeeded
            }
        }
        return false;//false otherwise
    }

    public void changeWeapon(WeaponBehaviour weapon)
    {
        //we are going with a quake style weapon changing system...
        //... we change the weapon instantly.
        currentWeapon = weapon;
    }
    public void setCanAttack(bool state)
    {
        //used with pausing / if the enemy is dead
        canAttack = state;
    }
}
