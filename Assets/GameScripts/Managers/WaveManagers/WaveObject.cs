using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wave/WaveObject")]
public class WaveObject : ScriptableObject{
	public WaveGroup[] groups;
	public float[] timeBetweenEachGroup;
	public void spawnWave(WaveManager manager, int currentGroup)
	{
		WaveGroup group = groups[currentGroup];
		group.spawnGroup(manager);
	}
	public int numEnemieInWave()
	{
		int total = 0;
		for(int i =0; i < groups.Length; i++)
		{
			total += groups[i].numEnemiesFromGroup();
		}
		return total;
	}
}
