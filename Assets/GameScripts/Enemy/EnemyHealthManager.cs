using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager {
	
	protected override void Start () {
        currentHealth = healthInfo.maxHealth;
	}
    public override void takeDamage(int damage)
    {
        currentHealth = HealthStats.reduceHealth(currentHealth, damage);
        if(currentHealth <= 0)
        {
            //guess i'll die
            Destroy(this.gameObject);
            //return to pool once we have that sorted
        }
        Debug.Log("Health: " + currentHealth);
        //base.takeDamage(damage);
    }
    public override void resetHealth()
    {
        currentHealth = healthInfo.maxHealth;//needed for pooling
    }
}
