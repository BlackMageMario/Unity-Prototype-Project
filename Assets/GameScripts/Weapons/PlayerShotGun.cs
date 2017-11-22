using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A shotgun weapon - uses raycasts to detect damage instead of projectiles
/// WeaponAttack method from: https://forum.unity.com/threads/creating-a-shotgun.83775/
/// Drawing the rays to the screen from: https://rockonflash.wordpress.com/2010/04/17/how-to-do-lasers-in-unity3d/
/// More info on rays: https://unity3d.com/learn/tutorials/projects/lets-try-assignments/lets-try-shooting-raycasts-article
/// </summary>
public class PlayerShotGun : WeaponBehaviour {
	//this is a hitscan weapon that shoots 9 raycasts in an area around the player
	//no need for a projectile
	public float maxRange;//maximum range of the shotgun 'pellets'
	public float maxAngleDev;//max angle deviation of the 'pellets'
	public BasicRayCast rayGraphics;//our raycast graphics
	private Camera shotgunCamera;
	protected override void Start()
	{
		//no need for projectiles
		source = gameObject.AddComponent<AudioSource>();
		source.maxDistance = 10f;
		source.spatialBlend = 1f;
		source.rolloffMode = AudioRolloffMode.Custom;
		source.volume = 1f;
		source.clip = soundForShot;
		source.playOnAwake = false;
		fireRate = weaponData.fireRate;
		magazineSize = weaponData.magazineSize;
		currentMagazine = magazineSize;
		reloadSpeed = weaponData.reloadSpeed;
		UIManager.instance.reloadMeter.maxValue = reloadSpeed;
		shotgunCamera = GetComponentInParent<Camera>();
		updateAmmoText();
		canFire = true;
	}
	private void updateAmmoText()
	{
		if (UIManager.instance)
		{
			UIManager.instance.ammoText.text = "Ammo Count:\n" + currentMagazine + "/" + magazineSize;
		}
	}
	protected override void weaponAttack()
	{
		for (int i = 0; i < 9; i++)
		{
            //We want to shoot our raycasts from the centre of the screen
            //with a bit of deviation
            //so we create a position based on the screen width and height
            //then set that point using Camera.ScreenPointToRay(Vector3);
			Vector3 position = new Vector3
				(Screen.width / 2 + Random.Range(-maxAngleDev, maxAngleDev),
					Screen.height / 2 + Random.Range(-maxAngleDev, maxAngleDev));
			Ray ray = shotgunCamera.ScreenPointToRay(position);
			//I could not get this working in the end and spent four hours
            //trying to get the graphics to display
            //I should investigate LineRender in my own time again
			//GameObject newRay = Instantiate(rayGraphics.gameObject,
				//firePoint.transform.position, 
					//Quaternion.Euler(1, -90, 1) * shotgunCamera.transform.rotation);
			//LineRenderer render = newRay.GetComponent<LineRenderer>();
			RaycastHit hit;//our raycast hit
			if(Physics.Raycast(ray, out hit, maxRange))
			{
				//render.SetPosition(0, shotgunCamera.transform.position);
				//render.SetPosition(1, shotgunCamera.transform.forward);
				//debug method that i used to ensure that the rays were working correctly
				Debug.DrawRay(shotgunCamera.ScreenToWorldPoint(position), ray.direction, Color.green, 5f);
				EnemyHealthManager manager = hit.transform.gameObject.GetComponent<EnemyHealthManager>();
				if(manager)
				{
					manager.takeDamage(weaponData.damage);
				}
			}
		}
		source.Play();
		currentMagazine -= 1;
	}
	protected override void OnEnable()
	{
		shotgunCamera = GetComponentInParent<Camera>();
		if (UIManager.instance)
		{
			UIManager.instance.reloadMeter.maxValue = reloadSpeed;
		}
		updateAmmoText();
		if (currentMagazine <= 0)
		{
			StartCoroutine(reloadMagazine());
			canFire = true;
		}
	}
	protected override void OnDisable()
	{
		StopAllCoroutines();
		UIManager.instance.reloadMeter.value = 0;
	}
}
