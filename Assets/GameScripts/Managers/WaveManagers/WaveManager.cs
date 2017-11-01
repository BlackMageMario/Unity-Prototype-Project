using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
	//wave manager will hold references to locations to spawn
	public WaveSpawner[] spawnPoints;
	public WaveObject[] waves;
	public float timeBeforeFirstWave;
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
	/*void Update()
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
	}*/	
	void OnTriggerEnter(Collider other)
	{
		if(other.name == "PlayerPrototype")
		{
			Debug.Log("Can Spawn");
			StartCoroutine(spawnWave());
			//canSpawn = true;
			Destroy(GetComponent<Collider>());
		}
	}
	IEnumerator spawnWave()
	{
		float currentTime = 0;
		float countdownTime = currentWave == 0 ? timeBeforeFirstWave : timeBetweenWaves;
		updateWaveTrackText();
		while (currentTime < countdownTime)
		{
			startWaveText(countdownTime - currentTime);
			yield return new WaitForSeconds(1f);
			currentTime += 1f;
		}
		StartCoroutine(waveStarted());
		int spawnGroup = 0;
		waveActive = true;
		Debug.Log("Beginnign wave: " + currentWave + 1);
		numEnemiesFromWave = waves[currentWave].numEnemieInWave();
		while(spawnGroup < waves[currentWave].groups.Length)
		{
			waves[currentWave].spawnWave(this, spawnGroup);
			
			//Debug.Log("spawnGroup: " + spawnGroup + " , groupsLength: " + waves[currentWave].groups.Length);
			//Debug.Log("Waves: " + waves[currentWave] + ", timeBetweenGroup: " + waves[currentWave].timeBetweenEachGroup[spawnGroup]);
			yield return new WaitForSeconds(waves[currentWave].timeBetweenEachGroup[spawnGroup]);
			spawnGroup++;
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
	private void startWaveText(float timeLeft)
	{
		//start back from here
		string countDownText = "Wave beginning in... ";
		UIManager.instance.waveAnnounceText.text = countDownText + timeLeft;
	}
	private IEnumerator waveStarted()
	{
		string waveStartedText = "Wave started!";
		UIManager.instance.waveAnnounceText.text = waveStartedText;
		yield return new WaitForSeconds(3f);
		UIManager.instance.waveAnnounceText.text = "";
	}
	private void updateWaveTrackText()
	{
		string updateText = "Wave: " + (currentWave+1) + "/" + waves.Length;
		UIManager.instance.waveTrackText.text = updateText;
	}
	IEnumerator startNewWaveAfterDelay()
	{
		UIManager.instance.waveAnnounceText.text = "Wave ended!";
		yield return new WaitForSeconds(timeBetweenWaves);
		UIManager.instance.waveAnnounceText.text = "";
		currentWave++;
		if(currentWave <waves.Length)
		{
			StartCoroutine(spawnWave());
		}
		else
		{
			//do something new
		}
	}
}
