using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour {
    public WeaponData weaponData;//this weapon's data
    public Camera bodyCamera;//maybe find camera instead of assigning it
    private WeaponProjectile projectile;// the projectile we fire - either just a bullet we show for visuals
    //or an actual projectile
    //projectiles will override our fireBullet method
    //private int damage;//the damage of the bullet - we pass this onto any weapon taht's a projectile
    private GameObject ammoTextUI;
    private Text ammoText;
    private float fireRate;
    private int magazineSize;//size of the magazine
    private int currentMagazine;//size of our magazine right now
    private float reloadSpeed;//reload speed of the gun
    private float spread;//spread of the gun - spread of 0 would be perfect shot every time
    // Use this for initialization
    private bool canFire;
    
    protected virtual void Start () {
        ammoTextUI = GameObject.Find("AmmoCount");
        ammoText = ammoTextUI.GetComponent<Text>();
        canFire = true;
        projectile = weaponData.projectile;
        fireRate = weaponData.fireRate;
        magazineSize = weaponData.magazineSize;
        currentMagazine = magazineSize;
        reloadSpeed = weaponData.reloadSpeed;
        spread = weaponData.spread;
        updateAmmoText();
	}

    public void fireGun()
    {
        if (canFire)
        {
            //we're going to use this as a hitscan weapon
            //but it doesn't have to be like this
            if (currentMagazine > 0)
            {
                weaponAttack();
                StartCoroutine(canFireWeapon());
            }
            else
            {
                StopCoroutine(canFireWeapon());//stop this
                StartCoroutine(reloadMagazine());
            }
            updateAmmoText();
        }
    }
    private void updateAmmoText()
    {
        //update the ammo text area
        ammoText.text = "Ammo Count:\n" + currentMagazine+"/"+magazineSize;
    }

    protected virtual void FixedUpdate()
    {
        //this can be overrided
        //depending on whether the weapon should be automatic
        //or single fire/burst

    }
    public virtual void weaponAttack()
    {
        //default code for hitscan weapon
        //we need to detect whether we have attacked something
        //first create projectile - in this instance it travels so fast you can't tell its not hitscan
        //NOTE: X -> side, Z-> in front in this example
        GameObject firedProjectile = Instantiate(projectile.gameObject, bodyCamera.transform.position /*+ new Vector3(0, 0, 0.5f)*/,
            bodyCamera.transform.rotation * Quaternion.Euler(0, -90, 0));
        firedProjectile.GetComponent<WeaponProjectile>().weaponStats(weaponData.damage, weaponData.projectileSpeed);
        currentMagazine -= 1;//take away a bullet
        Debug.Log(currentMagazine);
    }

    IEnumerator canFireWeapon()
    {
        canFire = false;
        yield return new WaitForSeconds(1/fireRate);
        canFire = true;
    }

    protected virtual IEnumerator reloadMagazine()//we want this to be overridable for things such as e.g. shotguns
    {
        //play a reload animation
        //while animation is playing, reload;
        Debug.Log("Reloading");
        canFire = false;
        yield return new WaitForSeconds(reloadSpeed);
        Debug.Log("Finished");
        currentMagazine = magazineSize;
        canFire = true;
        updateAmmoText();
    }


}
