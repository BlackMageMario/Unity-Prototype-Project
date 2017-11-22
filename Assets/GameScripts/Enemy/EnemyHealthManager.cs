using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Extends off HealthManager and overrides the methods
/// so that enemies can suitable use them
/// </summary>
public class EnemyHealthManager : HealthManager {
	
	protected override void Start ()
    {
        currentHealth = healthInfo.maxHealth;
	}
    public override void takeDamage(int damage)
    {
        //our take damage method is different: we only want to reduce health due to having no armour
        //and we return to the pool
        currentHealth = HealthStats.reduceHealth(currentHealth, damage);
        if(currentHealth <= 0)
        {
			EnemyManagerInfo info = GetComponent<EnemyManagerInfo>();//used with our wave manage 
			if(info)
			{
				info.manager.enemyDied();
			}
			PooledObject objectPool = GetComponent<PooledObject>();
			if(objectPool)//if it has an object pool
			{
				objectPool.pool.ReturnObject(this.gameObject);//return the object
			}
			else
			{
				Destroy(gameObject);//destroy it (expensive)
			}
        }
    }
    public override void resetHealth()
    {
        currentHealth = healthInfo.maxHealth;//needed for pooling
    }
}
