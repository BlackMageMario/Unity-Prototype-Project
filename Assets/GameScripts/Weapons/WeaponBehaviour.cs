﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour
{
    public WeaponData weaponData;//this weapon's data
    //public GameObject firePoint;//need to find point rather than doing it this way
	public int defaultProjectilesToPool;
    protected WeaponProjectile projectile;// the projectile we fire - either just a bullet we show for visuals
    //or an actual projectile
    //projectiles will override our fireBullet method
    //private int damage;//the damage of the bullet - we pass this onto any weapon taht's a projectile
    protected float fireRate;
    protected int magazineSize;//size of the magazine
    protected int currentMagazine;//size of our magazine right now
    protected float reloadSpeed;//reload speed of the gun
    protected float spread;//spread of the gun - spread of 0 would be perfect shot every time
	protected GameObject pool;
	protected bool canFire;
    private Camera weaponCamera;
    protected virtual void Start () {
        canFire = true;
        projectile = weaponData.projectile;
        fireRate = weaponData.fireRate;
        magazineSize = weaponData.magazineSize;
        currentMagazine = magazineSize;
        reloadSpeed = weaponData.reloadSpeed;
        spread = weaponData.spread;
		pool = ObjectPool.getPool(projectile.gameObject, defaultProjectilesToPool);
        UIManager.instance.reloadMeter.maxValue = reloadSpeed;
        weaponCamera = GetComponentInParent<Camera>();//need to change this line
        updateAmmoText();
	}
	protected virtual void OnEnable()
	{
		weaponCamera = GetComponentInParent<Camera>();
		UIManager.instance.reloadMeter.maxValue = reloadSpeed;
		updateAmmoText();
		if (currentMagazine <= 0)
		{
			StartCoroutine(reloadMagazine());
			canFire = true;//reloading weapon isn't enoguh - that doesn't mean we can fire again so set this here to true
		}
		
	}
	protected virtual void OnDisable()
	{
		StopAllCoroutines();//stop them all otherwise things go to shit
		UIManager.instance.reloadMeter.value = 0;
	}
    public virtual void fireGun()
    {
        if (canFire)
        {
            Debug.Log("Reached here. Ammo Count: " + currentMagazine);
            
            if (currentMagazine > 0)//this check *might* be unnecessary
            {
                weaponAttack();
                StartCoroutine(canFireWeapon());
            }
            updateAmmoText();
        }
    }
    private void updateAmmoText()
    {
        //update the ammo text area
        UIManager.instance.ammoText.text = "Ammo Count:\n" + currentMagazine + "/" + magazineSize; 
        //ammoText.text = "Ammo Count:\n" + currentMagazine+"/"+magazineSize;
    }

    protected virtual void FixedUpdate()
    {
        //this can be overrided
        //depending on whether the weapon should be automatic
        //or single fire/burst

    }
    protected virtual void weaponAttack()
    {
		//default code for projectile weapon
		//we need to detect whether we have attacked something
		//first create projectile - in this instance it travels so fast you can't tell its not hitscan
		//NOTE: X -> side, Z-> in front in this example
		GameObject firedProjectile = pool.GetComponent<ObjectPool>().spawnObject();
		//Debug.Log(weaponCamera.gameObject);
        firedProjectile.transform.position = weaponCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, (transform.localPosition.z + 1f)));
		firedProjectile.transform.rotation = weaponCamera.transform.rotation * Quaternion.Euler(1, -90, 1);
        projectile.GetComponent<Rigidbody>().AddForce(GetComponentInParent<Rigidbody>().velocity);
		firedProjectile.GetComponent<WeaponProjectile>().weaponStats(weaponData.damage, weaponData.projectileSpeed);
        currentMagazine -= 1;//take away a bullet
    }
	public int getCurrentMagazine()
	{
		return currentMagazine;
	}
    protected virtual IEnumerator canFireWeapon()
    {
        canFire = false;
        if(currentMagazine > 0)
        {
            yield return new WaitForSeconds(1 / fireRate);   
        }
        else
        {
            yield return StartCoroutine(reloadMagazine());//wait for this routine to finish
        }
        canFire = true;
    }

    protected virtual IEnumerator reloadMagazine()//we want this to be overridable for things such as e.g. shotguns
    {
        //play a reload animation
        //while animation is playing, reload;
        canFire = false;
        float reloadTimer = 0;
        while (reloadTimer < reloadSpeed)
        {
            UIManager.instance.reloadMeter.value += Time.deltaTime;
            reloadTimer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        UIManager.instance.reloadMeter.value = 0;
        currentMagazine = magazineSize;
		updateAmmoText();
	}
}
