using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Health Manager class. Its default implementation points to it being used for the player
/// It is designed to be extended by other classes that need health and needs to change their behaviours
/// based on their health
/// </summary>
public class HealthManager : MonoBehaviour {
    public HealthStats healthInfo;//our health stat
    protected int currentHealth;//keep track of our health and armour through the manager
    protected int currentArmour;
    private PlayerMovement playerMovement;//need the player's movement
    private WeaponManager weaponManager;//need the player's weapon manager
	// Use this for initialization
	protected virtual void Start () {
        //initalise all the values we need
        currentHealth = healthInfo.spawnHealth;
        currentArmour = healthInfo.spawnArmour;
        playerMovement = GetComponent<PlayerMovement>();
        weaponManager = GetComponent<WeaponManager>();
        //references to our UIManager
        UIManager.instance.healthMeter.maxValue = healthInfo.maxHealth;
        UIManager.instance.healthMeter.value = currentHealth;
        UIManager.instance.armourMeter.maxValue = healthInfo.maxArmour;
        UIManager.instance.armourMeter.value = currentArmour;
        enabled = true;
	}
    public virtual void takeDamage(int damage)
    {
        //reduce our health
        currentHealth = HealthStats.armourReduceHealth(currentHealth, currentArmour, damage);
        //then reduce our armour
        currentArmour = HealthStats.reduceArmour(currentArmour, damage);
        if (currentHealth <= 0)
        {
            //set the state of our GameStateManager singleton to the dead state
			GameStateManager.instance.PlayerDead();
        }
        UIManager.instance.healthMeter.value = currentHealth;
        UIManager.instance.armourMeter.value = currentArmour;
    }

    public virtual void increaseHealth(int heal)
    {
        //increase health
        currentHealth = HealthStats.increaseHealth(currentHealth, healthInfo.maxHealth, heal);
        UIManager.instance.healthMeter.value = currentHealth;
    }
    public virtual void increaseArmour(int armourHeal)
    {
       //increase armour
       currentArmour = HealthStats.increaseArmour(currentArmour, healthInfo.maxArmour, armourHeal);
       UIManager.instance.armourMeter.value = currentArmour;
    }

    public virtual void resetHealth()
    {
        //reset all our health values to the defeault - used when resetting the game
        currentHealth = healthInfo.spawnHealth;
        currentArmour = healthInfo.spawnArmour;
        UIManager.instance.healthMeter.value = currentHealth;
        UIManager.instance.armourMeter.value = currentArmour;
        playerMovement.setFreeze(false);
        weaponManager.setCanAttack(true);
        enabled = true;
    }
}
