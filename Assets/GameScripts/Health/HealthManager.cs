using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    public HealthStats healthInfo;
    protected int currentHealth;
    protected int currentArmour;
    private PlayerMovement playerMovement;//this is the player's
    //health manager, so have the player movement ready
    private WeaponManager weaponManager;
	// Use this for initialization
	protected virtual void Start () {
        currentHealth = healthInfo.spawnHealth;
        currentArmour = healthInfo.spawnArmour;
        playerMovement = GetComponent<PlayerMovement>();
        weaponManager = GetComponent<WeaponManager>();
        UIManager.instance.healthMeter.maxValue = healthInfo.maxHealth;
        UIManager.instance.healthMeter.value = currentHealth;
        enabled = true;
	}
    public virtual void takeDamage(int damage)
    {
        currentHealth = HealthStats.armourReduceHealth(currentHealth, currentArmour, damage);
        currentArmour = HealthStats.reduceArmour(currentArmour, damage);
        if (currentHealth < 0)
        {

            //body.Sleep();
            //guess i'll die (die)
            playerMovement.setFreeze(true);
            weaponManager.setCanAttack(false);
        }
        UIManager.instance.healthMeter.value = currentHealth;
        Debug.Log("Health: " + currentHealth);
    }

    public virtual void increaseHealth(int heal)
    {
        currentHealth = HealthStats.increaseHealth(currentHealth, healthInfo.maxHealth, heal);
    }
    public virtual void increaseArmour(int armourHeal)
    {
       currentArmour = HealthStats.increaseArmour(currentArmour, healthInfo.maxArmour, armourHeal);
    }

    public virtual void resetHealth()
    {
        currentHealth = healthInfo.spawnHealth;
        currentArmour = healthInfo.spawnArmour;
        playerMovement.setFreeze(false);
        weaponManager.setCanAttack(true);
        enabled = true;
    }

}
