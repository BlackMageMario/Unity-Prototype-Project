using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wave/WaveGroup")]
public class WaveGroup : ScriptableObject {
	
	public GameObject[] enemiesToSpawn;
	public int[] numToSpawn;
	public bool oneByOne;
	public float oneByOneRate;
	public void spawnGroup(WaveManager manager)
	{
		if (oneByOne)
		{
			manager.StartCoroutine(spawnOneByOne(manager));		
		}
		else
		{
			spawnEnemies(manager);
		}

	}
	private void spawnEnemies(WaveManager manager)
	{
		Random.InitState((int)Time.time);
		GameObject pool;
		for(int i = 0; i < enemiesToSpawn.Length; i++)
		{
			GameObject associatedPool = GameObject.Find("Pool: " + enemiesToSpawn[i].name);
			if (associatedPool)
			{
				pool = associatedPool;
			}
			else
			{
				pool = new GameObject("Pool: " + enemiesToSpawn[i].name);
				pool.AddComponent<ObjectPool>();
				pool.GetComponent<ObjectPool>().setUpPool(enemiesToSpawn[i].gameObject, numToSpawn[i]);
			}
			Debug.Log("Pool in spawn enemies: " + pool);
			for (int j = 0; j < numToSpawn[i]; j++)
			{
				spawnAnEnemy(manager, pool);
			}
		}
	}
	public int numEnemiesFromGroup()
	{
		int total = 0;
		for(int i = 0; i < enemiesToSpawn.Length; i++)
		{
			total += numToSpawn[i];
		}
		return total;
	}
	private IEnumerator spawnOneByOne(WaveManager manager)
	{
		Random.InitState((int)Time.time);
		Dictionary<GameObject, GameObject> pools = new Dictionary<GameObject, GameObject>();
		for(int i = 0; i < enemiesToSpawn.Length; i++)
		{
			GameObject associatedPool = GameObject.Find("Pool: " + enemiesToSpawn[i].name);
			if (associatedPool)
			{
				pools.Add(enemiesToSpawn[i], associatedPool);
			}
			else
			{
				associatedPool = new GameObject("Pool: " + enemiesToSpawn[i].name);
				associatedPool.AddComponent<ObjectPool>();
				associatedPool.GetComponent<ObjectPool>().setUpPool(enemiesToSpawn[i].gameObject, numToSpawn[i]);
				pools.Add(enemiesToSpawn[i], associatedPool);
			}
		}
		yield return new WaitForSeconds(1f);
		for(int i = 0; i < enemiesToSpawn.Length; i++)
		{
			for(int j = 0; j <numToSpawn[i]; j++)
			{
				GameObject pool;
				pools.TryGetValue(enemiesToSpawn[i], out pool);
				spawnAnEnemy(manager, pool);
				yield return new WaitForSeconds(oneByOneRate);
			}
		}
	}
	private GameObject getPool(GameObject enemy, int numToSpawn)
	{
		GameObject associatedPool = GameObject.Find("Pool: " + enemy.name);
		if(associatedPool)
		{
			return associatedPool;
		}
		else
		{
			associatedPool = new GameObject("Pool: " + enemy.name);
			associatedPool.AddComponent<ObjectPool>();
			associatedPool.GetComponent<ObjectPool>().setUpPool(enemy.gameObject, numToSpawn);
			return associatedPool;
		}
	}
	private void spawnAnEnemy(WaveManager manager, GameObject pool)
	{
		Random.InitState((int)Time.time);
		Debug.Log("The pool: " + pool);
		GameObject enemyToSpawn = pool.GetComponent<ObjectPool>().spawnObject();
		EnemyManagerInfo enemyManagerInfo = enemyToSpawn.AddComponent<EnemyManagerInfo>();
		enemyManagerInfo.manager = manager;
		enemyToSpawn.transform.position =
			manager.spawnPoints[Random.Range(0, manager.spawnPoints.Length)].randomSpawnPoint();
	}
}

public class EnemyManagerInfo : MonoBehaviour
{
	public WaveManager manager;
}
