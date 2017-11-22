using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This teleport behhaviour allows the enemy to teleport around to different points on the maps
/// </summary>
public class TeleportBehaviour : MonoBehaviour {

	public GameObject[] teleportPoints;//array of gameobjects on the map to teleport to
    //this could be a scriptable object for each map to create that takes in these points
	public float teleportCooldown;//cooldown of teleport
	private bool canTeleport;//determines whether we can teleport at this moment or not
	void Start () {
		Random.InitState((int)System.DateTime.Now.Ticks);
		canTeleport = true;
	}
	public void Teleport()
	{
		if(canTeleport)
		{
            //get the random point we want
			int teleportPoint = Random.Range(0, teleportPoints.Length);
			StartCoroutine(TeleportAnimation(teleportPoints[teleportPoint].transform.position));
		}
		
	}
	IEnumerator TeleportAnimation(Vector3 teleportPosition)
	{
		canTeleport = false;
        //we temporarily set the position of the object to somewhere WAY above the world
        //we could easily set it anywehre outside the arena
		transform.position = new Vector3(0, 1000, 0);
		yield return new WaitForSeconds(3f);
		//now teleport to our new place
		transform.position = teleportPosition;
		StartCoroutine(CanTelportAgain());
	}
	IEnumerator CanTelportAgain()
	{
        //set the teleport on cooldown
		yield return new WaitForSeconds(teleportCooldown);
		canTeleport = true;
	}
}
