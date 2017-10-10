using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public HealthStats healthInfo;
    protected int currentHealth;
    protected int currentArmour;
	// Use this for initialization
	protected virtual void Start () {
        currentHealth = healthInfo.spawnHealth;
        currentArmour = healthInfo.spawnArmour;
	}
    public virtual void takeDamage(int damage)
    {
        currentHealth = HealthStats.armourReduceHealth(currentHealth, currentArmour, damage);
        currentArmour = HealthStats.reduceArmour(currentArmour, damage);
        if (currentHealth < 0)
        {
            //guess i'll die (die)
        }
    }

    public virtual void increaseHealth(int heal)
    {
        currentHealth = HealthStats.increaseHealth(currentHealth, healthInfo.maxHealth, heal);
    }
    public virtual void increaseArmour(int armourHeal)
    {
       currentArmour = HealthStats.increaseArmour(currentArmour, healthInfo.maxArmour, armourHeal);
    }
}
