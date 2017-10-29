using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Using: http://answers.unity3d.com/questions/714835/best-way-to-spawn-prefabs-in-a-circle.html
/// as a random point
/// </summary>
public class WaveSpawner : MonoBehaviour {
	public float rad;
	// Use this for initialization
	void Start () {
		
	}
	public Vector3 randomSpawnPoint()
	{
		Random.InitState((int)System.DateTime.Now.Ticks);
		float angle = Random.value * 360;
		Vector3 position = new Vector3(transform.position.x + rad * Mathf.Sin(angle * Mathf.Deg2Rad),
			transform.position.y, transform.position.z * Mathf.Sin(angle * Mathf.Deg2Rad));
		return position;
	}
}
