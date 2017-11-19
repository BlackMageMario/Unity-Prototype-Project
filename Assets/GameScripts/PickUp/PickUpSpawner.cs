using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour {
	public PickUpDetails[] ItemsToSpawn;
	private List<PickUpDetails> oneSpawnOnly;
	//scriptable object?
	public GameObject[] spawnLocation;//locations to spawn at
	public float spawnRate; // i.e. how often to spawn in seconds
	//the items we want to spawn
	//um... yeah.
	//w'll call this from the wave manager
	void Start () 
	{
		Random.InitState((int)System.DateTime.Now.Ticks);
		oneSpawnOnly = new List<PickUpDetails>();
	}
	public void startSpawning()
	{
		StartCoroutine(spawnItem());
	}
	public void stopSpawning()
	{
		StopAllCoroutines();
	}
	IEnumerator spawnItem()
	{
		Debug.Log("ITEM SPAWNING HAS BEGUN");
		float currentTime = 0;
		while(true)
		{
			if (currentTime < spawnRate)
			{
				yield return new WaitForSeconds(.1f);
				currentTime += .1f;
			}
			else
			{
				//now we can spawn
				int itemWanted;
				do
				{
					itemWanted = Random.Range(0, ItemsToSpawn.Length);
				}
				while (oneSpawnOnly.Contains(ItemsToSpawn[itemWanted]));//if its not we can spawn it
				Debug.Log("Item wanted: " + itemWanted);
				GameObject toSpawn = Instantiate(ItemsToSpawn[itemWanted].ourPickup.gameObject);
				int spawner = Random.Range(0, spawnLocation.Length);
				toSpawn.transform.position = spawnLocation[spawner].transform.position;	
				if(!ItemsToSpawn[itemWanted].spawnContinuous)
				{
					//if we only want to spawn it once - add it to onespawnonly list
					oneSpawnOnly.Add(ItemsToSpawn[itemWanted]);
				}
				currentTime = 0;
			}
		}
	}
}
