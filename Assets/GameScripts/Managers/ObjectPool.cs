using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Based on SimpleObjectPool script from https://unity3d.com/learn/tutorials/topics/user-interface-ui/intro-and-setup?playlist=17111
/// Just an object pool - much better than creating and destroying a ton of asteroids which can cause various issues
/// </summary>
public class ObjectPool : MonoBehaviour
{
	public GameObject objectToPool;
	public int defaultNumber;
	private Stack<GameObject> inactiveObjects = new Stack<GameObject>();
	// Use this for initialization
	/*void Start()
	{
		for (int i = 0; i < defaultNumber; i++)
		{
			GameObject spawnedObject = (GameObject)GameObject.Instantiate(objectToPool);
			spawnedObject.transform.SetParent(this.gameObject.transform);
			spawnedObject.SetActive(false);
			PooledObject pooledObject = spawnedObject.AddComponent<PooledObject>();
			pooledObject.pool = this;
			inactiveObjects.Push(spawnedObject);
			//not finding parent

		}
	}*/
	public void setUpPool(GameObject objectToPool, int defaultNumber)
	{
		this.objectToPool = objectToPool;
		this.defaultNumber = defaultNumber;
		for (int i = 0; i < defaultNumber; i++)
		{
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

