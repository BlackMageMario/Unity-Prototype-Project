using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour {
    public int numToSpawn;
    protected GameObject pool;

	// Use this for initialization
	protected virtual void Start () {
        pool = ObjectPool.getPool(gameObject, numToSpawn);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
    protected virtual void OnTriggerEnter(Collider other)
    {
        //check if the collider is the player
        if(other.name == "PlayerPrototype")
        {
            executeAction(other.gameObject);
            PooledObject pooledObject = GetComponent<PooledObject>();
            if (pooledObject)
            {
                pooledObject.pool.ReturnObject(this.gameObject);
            }
            else
            {
                //no pool - destroy
                //set inactive
                gameObject.SetActive(false);
            }
        }
    }
    protected abstract void executeAction(GameObject execute);
}

