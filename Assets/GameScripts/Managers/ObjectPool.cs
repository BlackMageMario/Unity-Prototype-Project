using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Based on SimpleObjectPool script from https://unity3d.com/learn/tutorials/topics/user-interface-ui/intro-and-setup?playlist=17111
/// Just an object pool - much better than creating and destroying a ton of objects
/// which causes performance issues
/// </summary>
public class ObjectPool : MonoBehaviour
{
	public GameObject objectToPool;
	private Stack<GameObject> inactiveObjects = new Stack<GameObject>();
	public static GameObject getPool(GameObject prefab, int numToSpawn)
	{
		//Debug.Log("Num to spawn: " + numToSpawn);
		GameObject associatedPool = GameObject.Find("Pool: " + prefab.name);
		if (associatedPool)
		{
			return associatedPool;
		}
		else
		{
			associatedPool = new GameObject("Pool: " + prefab.name);
			associatedPool.AddComponent<ObjectPool>();
			associatedPool.GetComponent<ObjectPool>().setUpPool(prefab.gameObject, numToSpawn);
			return associatedPool;
		}
	}
	public void setUpPool(GameObject objectToPool, int defaultNumber)
	{
		this.objectToPool = objectToPool;
		//this.defaultNumber = defaultNumber;
		
		for (int i = 0; i < defaultNumber; i++)
		{
			//Debug.Log("i: " + i);
			GameObject spawnedObject = (GameObject)GameObject.Instantiate(objectToPool);
			spawnedObject.transform.SetParent(this.gameObject.transform);
			spawnedObject.SetActive(false);
			PooledObject pooledObject = spawnedObject.AddComponent<PooledObject>();
			pooledObject.pool = this;
			inactiveObjects.Push(spawnedObject);
			//not finding parent
		}
	}
	public GameObject spawnObject()
	{
		GameObject spawnedObject;
		if (inactiveObjects.Count > 0)
		{
			//take something off the top of the stack
			spawnedObject = inactiveObjects.Pop();
		}
		else
		{
			//we need a new instance
			spawnedObject = (GameObject)GameObject.Instantiate(objectToPool);
			spawnedObject.transform.SetParent(this.gameObject.transform);
			PooledObject pooledObject = spawnedObject.AddComponent<PooledObject>();
			pooledObject.pool = this;
		}
		spawnedObject.SetActive(true);
		return spawnedObject;
	}

	public void ReturnObject(GameObject toReturn)
	{
		PooledObject pooledObject = toReturn.GetComponent<PooledObject>();
		if (pooledObject != null && pooledObject.pool == this)
		{
			toReturn.SetActive(false);
			inactiveObjects.Push(toReturn);
		}
		else
		{
			//sent to wrong pool
			Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from.");
			Destroy(toReturn);
		}
	}
}

public class PooledObject : MonoBehaviour
{
	public ObjectPool pool;
}

