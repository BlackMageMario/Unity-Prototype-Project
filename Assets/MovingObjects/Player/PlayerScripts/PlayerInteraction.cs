using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	private Camera bodyCamera;
	public GameObject projectiles;
	private bool canFire;
	void Start () {
		canFire = true;
		bodyCamera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.Mouse0) && canFire)
		{
			Instantiate(projectiles, transform.position + new Vector3(0, 1, 0), bodyCamera.transform.rotation * Quaternion.Euler(0, -90, 0));
			StartCoroutine(fireRate());
		}
		//bodyCamera.
	}
	IEnumerator fireRate()
	{
		canFire = false;
		yield return new WaitForSeconds(0.5f);
		canFire = true;
	}
}
