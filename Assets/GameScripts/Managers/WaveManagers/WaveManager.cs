using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
	//wave manager will hold references to locations to spawn
	public WaveSpawner[] spawnPoints;
	public WaveObject[] waves;
	public float timeBetweenWaves;
	private bool canSpawn;
	private int currentWave;
	private bool waveActive;
	private int numEnemiesFromWave = 0;
	void Start()
	{
		currentWave = 0;
		canSpawn = false;
	}
	void Update()
	{
		if(canSpawn)
		{
			if(!waveActive)
			{
				//if there is no wave active
				//spawn one
				if(currentWave < waves.Length)
				{
					StartCoroutine(spawnWave());
				}
				else
				{
					//we have won, execute an action i suppose
					canSpawn = false;
				}
			}
		}
	}	
	void OnTriggerEnter(Collider other)
	{
		if(other.name == "PlayerPrototype")
		{
			Debug.Log("Can Spawn");
			canSpawn = true;
		}
	}
	IEnumerator spawnWave()
	{
		int spawnGroup = 0;
		waveActive = true;
		Debug.Log("Beginnign wave: " + currentWave + 1);
		numEnemiesFromWave = waves[currentWave].numEnemieInWave();
		while(spawnGroup < waves[currentWave].groups.Length)
		{
			waves[currentWave].spawnWave(this, spawnGroup);
			
			Debug.Log("spawnGroup: " + spawnGroup + " , groupsLength: " + waves[currentWave].groups.Length);
			Debug.Log("Waves: " + waves[currentWave] + ", timeBetweenGroup: " + waves[currentWave].timeBetweenEachGroup[spawnGroup]);
			yield return new WaitForSeconds(waves[currentWave].timeBetweenEachGroup[spawnGroup]);
			spawnGroup++;
			//be at the end
		}
	}
	
	public void enemyDied()
	{
		numEnemiesFromWave -= 1;
		if(numEnemiesFromWave <= 0)
		{
			//we can start a new wave
			StartCoroutine(startNewWaveAfterDelay());
			
		}
	}
	IEnumerator startNewWaveAfterDelay()
	{
		yield return new WaitForSeconds(timeBetweenWaves);
		currentWave++;
		waveActive = false;
	}
}
