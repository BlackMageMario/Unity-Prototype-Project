using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// WeaponAttack method from: https://forum.unity.com/threads/creating-a-shotgun.83775/
/// Drawing the rays to the screen from: https://rockonflash.wordpress.com/2010/04/17/how-to-do-lasers-in-unity3d/
/// More info on rays: https://unity3d.com/learn/tutorials/projects/lets-try-assignments/lets-try-shooting-raycasts-article
/// </summary>
public class PlayerShotGun : WeaponBehaviour {

	// Use this for initialization
	//this is a hitscan weapon that shoots 9 raycasts in an area around the player
	//no need for a projectile
	public float maxRange;
	public float maxAngleDev;
	public BasicRayCast rayGraphics;
	//public WeaponProjectile rayGraphics;
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
		//we don't need spread
		UIManager.instance.reloadMeter.maxValue = reloadSpeed;
		shotgunCamera = GetComponentInParent<Camera>();
		updateAmmoText();
		canFire = true;
	}
	private void updateAmmoText()
	{
		//update the ammo text area
		if (UIManager.instance)
		{
			UIManager.instance.ammoText.text = "Ammo Count:\n" + currentMagazine + "/" + magazineSize;
		}
	}
	protected override void weaponAttack()
	{
		for (int i = 0; i < 9; i++)
		{
			Vector3 position = new Vector3
				(Screen.width / 2 + Random.Range(-maxAngleDev, maxAngleDev),
					Screen.height / 2 + Random.Range(-maxAngleDev, maxAngleDev));
			Ray ray = shotgunCamera.ScreenPointToRay(position);
			//I could not get this working in the end and wasted at-least four hours here.
			//GameObject newRay = Instantiate(rayGraphics.gameObject,
				//firePoint.transform.position, 
					//Quaternion.Euler(1, -90, 1) * shotgunCamera.transform.rotation);
			//LineRenderer render = newRay.GetComponent<LineRenderer>();
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, maxRange))
			{
				//render.SetPosition(0, shotgunCamera.transform.position);
				//render.SetPosition(1, shotgunCamera.transform.forward);
				
				Debug.DrawRay(shotgunCamera.ScreenToWorldPoint(position), ray.direction, Color.green, 5f);
				EnemyHealthManager manager = hit.transform.gameObject.GetComponent<EnemyHealthManager>();
				if(manager)
				{
					manager.takeDamage(weaponData.damage);
				}
				
				//this is more random
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
			canFire = true;//reloading weapon isn't enoguh - that doesn't mean we can fire again so set this here to true
		}

	}
	protected override void OnDisable()
	{
		StopAllCoroutines();//stop them all otherwise things go to shit
		UIManager.instance.reloadMeter.value = 0;
	}
}
