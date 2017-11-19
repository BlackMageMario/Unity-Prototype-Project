using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehaviour : MonoBehaviour {

	public GameObject[] teleportPoints;
	// Use this for initialization
	public float teleportCooldown;
	private bool canTeleport;
	void Start () {
		Random.InitState((int)System.DateTime.Now.Ticks);
		canTeleport = true;
		//need a way to get all the points programmatically
	}
	
	// Update is called once per frame
	public void Teleport()
	{
		if(canTeleport)
		{
			int teleportPoint = Random.Range(0, teleportPoints.Length);
			StartCoroutine(TeleportAnimation(teleportPoints[teleportPoint].transform.position));
		}
		
	}
	IEnumerator TeleportAnimation(Vector3 teleportPosition)
	{
		//there might be an actual animation here - if I get to it
		//play an animation
		//and then move the thing out of the world for like. Seven centuries
		canTeleport = false;
		transform.position = new Vector3(0, 1000, 0);
		yield return new WaitForSeconds(3f);
		//now teleport to our new place
		transform.position = teleportPosition;
		StartCoroutine(CanTelportAgain());
	}
	IEnumerator CanTelportAgain()
	{
		yield return new WaitForSeconds(teleportCooldown);
		canTeleport = true;
	}
}
