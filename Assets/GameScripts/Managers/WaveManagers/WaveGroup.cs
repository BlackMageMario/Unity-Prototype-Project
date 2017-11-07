using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// http://answers.unity3d.com/questions/452128/is-it-possible-to-check-if-an-object-exists-at-a-s.html
/// </summary>
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
			//this is a workaround for the fact that
			//scriptable objects can't start coroutines
			//... pass in an object that can ;)
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
			pool = ObjectPool.getPool(enemiesToSpawn[i].gameObject, numToSpawn[i]);
			//Debug.Log("Pool in spawn enemies: " + pool);
			for (int j = 0; j < numToSpawn[i]; j++)
			{
				Random.InitState((int)Time.time);
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
		Random.InitState((int)System.DateTime.Now.Ticks);
		Dictionary<GameObject, GameObject> pools = new Dictionary<GameObject, GameObject>();
		for(int i = 0; i < enemiesToSpawn.Length; i++)
		{
			GameObject associatedPool = ObjectPool.getPool(enemiesToSpawn[i], numToSpawn[i]);
			pools.Add(enemiesToSpawn[i], associatedPool);
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
	private void spawnAnEnemy(WaveManager manager, GameObject pool)
	{
		
		//Debug.Log("The pool: " + pool);
		GameObject enemyToSpawn = pool.GetComponent<ObjectPool>().spawnObject();
		EnemyManagerInfo enemyManagerInfo = enemyToSpawn.AddComponent<EnemyManagerInfo>();
		enemyManagerInfo.manager = manager;
		bool uniquePos = true;
		Vector3 randPos = new Vector3(0, 0, 0);
		//TODO: find another way of doing this
		//since technically spanwers should be responsible for spwaning
		//an enemy
		while (!uniquePos)
		{
			int rand = Random.Range(0, manager.spawnPoints.Length);
			randPos = manager.spawnPoints[rand].randomSpawnPoint();
			//set twenty to something else i guess at some point
			Collider[] hitColliders = Physics.OverlapSphere(randPos, 20);
			for(int i =0; i < hitColliders.Length; i++)
			{
				if(!(hitColliders[i].gameObject.name == enemyToSpawn.name))
				{
					uniquePos = true;
					//we did not detect any other enemy
				}
			}
		}
		//Debug.Log("position: " + randPos);
		enemyToSpawn.transform.position = randPos;
			
	}
}

public class EnemyManagerInfo : MonoBehaviour
{
	public WaveManager manager;
}
