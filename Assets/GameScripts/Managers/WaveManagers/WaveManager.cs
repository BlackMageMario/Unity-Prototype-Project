using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
	//wave manager will hold references to locations to spawn
	public WaveSpawner[] spawnPoints;
	public WaveObject[] waves;
	public float timeBeforeFirstWave;
	public float timeBetweenWaves;
	public MovingPlatformManager movePlatforms;
	public PickUpSpawner itemSpawner;
	private bool canSpawn;
	private int currentWave;
	private bool waveActive;
	private int numEnemiesFromWave = 0;
	void Start()
	{
		currentWave = 0;
		canSpawn = false;
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.name == "PlayerPrototype")
		{
			Debug.Log("Can Spawn");
			StartCoroutine(spawnWave());
			itemSpawner.startSpawning();
			//canSpawn = true;
			Destroy(GetComponent<Collider>());
		}
	}
	IEnumerator spawnWave()
	{
		float currentTime = 0;
		float countdownTime = currentWave == 0 ? timeBeforeFirstWave : timeBetweenWaves;
		updateWaveTrackText();
		GameState state;
		while (currentTime < countdownTime)
		{
			state = GameStateManager.instance.GetCurrentGameState();
			if (state != GameState.DEAD)
			{
				startWaveText(countdownTime - currentTime);
				yield return new WaitForSeconds(1f);
				currentTime += 1f;
			}
			else
			{
				yield return new WaitForFixedUpdate();
			}
		}
		StartCoroutine(waveStarted());
		int spawnGroup = 0;
		waveActive = true;
		Debug.Log("Beginnign wave: " + currentWave + 1);
		numEnemiesFromWave = waves[currentWave].numEnemieInWave();
		while(spawnGroup < waves[currentWave].groups.Length)
		{
			state = GameStateManager.instance.GetCurrentGameState();
			if (state != GameState.DEAD)
			{
				waves[currentWave].spawnWave(this, spawnGroup);
				yield return new WaitForSeconds(waves[currentWave].timeBetweenEachGroup[spawnGroup]);
				spawnGroup++;
			}
			else
			{
				yield return new WaitForFixedUpdate();
			}

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
		GameState state;
		float currentTime = 0f;
		while(currentTime < 3f)
		{
			state = GameStateManager.instance.GetCurrentGameState();
			if (state != GameState.DEAD)
			{
				yield return new WaitForSeconds(.1f);
				currentTime += .1f;
			}
			else
			{
				yield return new WaitForFixedUpdate();
			}

		}
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

		movePlatforms.callGroups();
		float currentTime = 0f;
		GameState state;
		while (currentTime < timeBetweenWaves)
		{
			state = GameStateManager.instance.GetCurrentGameState();
			if (state != GameState.DEAD)
			{
				yield return new WaitForSeconds(.1f);
				currentTime += .1f;
			}
			else
			{
				yield return new WaitForFixedUpdate();
			}
				
		}
		UIManager.instance.waveAnnounceText.text = "";
		currentWave++;
		if(currentWave <waves.Length)
		{
			StartCoroutine(spawnWave());
		}
		else
		{
			//do something new
			//probably open teh final gate
			itemSpawner.stopSpawning();
		}
	}
}
