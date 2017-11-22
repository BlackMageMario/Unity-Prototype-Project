using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Our actual weapon implementation. This class is designed to be extended for enemy weapons
/// and other types of weapons that are not simple projectile spawning weapons
/// https://answers.unity.com/questions/1075436/weapon-rotating-in-a-weird-way.html for weapon rotation
/// http://www2.it.nuigalway.ie/~sredfern/ct3111/07_Part7.pdf - sound notes
/// </summary>
public class WeaponBehaviour : MonoBehaviour
{
    public WeaponData weaponData;//this weapon's data and statistics
	public AudioClip soundForShot;//the sound clip associated with the weapon
	public int defaultProjectilesToPool;//the number of projectiles we want to use with object pooling
	public Vector3 preferredPosition;//used with the player manager to set the object to its correct
    //local position when attached to the player camera
    public Vector3 preferredScale;//used with the player managed to set the object to its correct scale
	public GameObject firePoint;//where the gun fires from
	protected AudioSource source;//we create this due to the object
    protected WeaponProjectile projectile;// the projectile we fire - either just a bullet we show for visuals
    //or an actual projectile
    protected float fireRate;//the firerate
    protected int magazineSize;//size of the magazine
    protected int currentMagazine;//size of our magazine right now
    protected float reloadSpeed;//reload speed of the gun
    protected float spread;//spread of the gun - spread of 0 would be perfect shot every time
	protected GameObject pool;//the pool associated with this weapon
	protected bool canFire;//determines whether we can fire
	protected Quaternion originalRotation;//the original rotation of the weapon
    private Camera weaponCamera;
    protected virtual void Start () {
		source = gameObject.AddComponent<AudioSource>();//create our audio source and set defaults
		source.maxDistance = 10f;
		source.spatialBlend = 1f;
		source.rolloffMode = AudioRolloffMode.Custom;
		source.volume = 1f;
		source.clip = soundForShot;
		source.playOnAwake = false;
		originalRotation = transform.rotation;//set the rotation of the weapon
        //to our original rotation
        projectile = weaponData.projectile;
        fireRate = weaponData.fireRate;
        magazineSize = weaponData.magazineSize;
        currentMagazine = magazineSize;
        reloadSpeed = weaponData.reloadSpeed;
        spread = weaponData.spread;
		pool = ObjectPool.getPool(projectile.gameObject, defaultProjectilesToPool);//set up our object pool
		weaponCamera = GetComponentInParent<Camera>();
		canFire = true;
	}
	protected virtual void OnEnable()
	{
        //when the weapon is enabled for players
		weaponCamera = GetComponentInParent<Camera>();
		if(UIManager.instance)
		{
			UIManager.instance.reloadMeter.maxValue = reloadSpeed;
		}
		
		updateAmmoText();
		if (currentMagazine <= 0)
		{
			StartCoroutine(reloadMagazine());
			canFire = true;//reloading weapon isn't enough - that doesn't mean we can fire again so set this here to true
		}
		
	}
	protected virtual void OnDisable()
	{
		StopAllCoroutines();//stop all our coroutines for now
		UIManager.instance.reloadMeter.value = 0;
	}
    public virtual void fireGun()
    {
        if (canFire)
        {   
            if (currentMagazine > 0)//this check might be unecessary due to other changes
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
		if(UIManager.instance)
		{
			UIManager.instance.ammoText.text = "Ammo Count:\n" + currentMagazine + "/" + magazineSize;
		}
    }

    protected virtual void weaponAttack()
    {
		//default code for projectile weapon
		//we need to detect whether we have attacked something
		//first create projectile - in this instance it travels so fast you can't tell its not hitscan
		//NOTE: X -> side, Z-> in front in this example
		GameObject firedProjectile = pool.GetComponent<ObjectPool>().spawnObject();
		firedProjectile.transform.position = firePoint.transform.position;
        //although this code is now dimied out, for raycast weapons this would be preferable:
        //fire the weapon at the centre of the camera - at the 'crosshair'
        //this does not work for projectiles however
		//firedProjectile.transform.position = weaponCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, (weaponCamera.transform.localPosition.z + 1f)));
		firedProjectile.transform.rotation = weaponCamera.transform.rotation * Quaternion.Euler(1, -90, 1);
        //we have to protate the projectile to the correct position
        Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), GetComponentInParent<Collider>());
        //we do not want the bullet to collide with its creator
        projectile.GetComponent<Rigidbody>().AddForce(GetComponentInParent<Rigidbody>().velocity);
        //get the projectile we spawned and set its states before its behaviours activate
		firedProjectile.GetComponent<WeaponProjectile>().weaponStats(weaponData.damage, weaponData.projectileSpeed);
		source.Play();
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
            //return 1 / fireRate
            //fireRate is measured in rounds per second
            yield return new WaitForSeconds(1 / fireRate);   
        }
        else
        {
            yield return StartCoroutine(reloadMagazine());//wait for this routine to finish
        }
        canFire = true;
    }

    protected virtual IEnumerator reloadMagazine()//we want this to be overridable for things such as
     //revolvers that reload one bullet at a time
    {
        canFire = false;
        float reloadTimer = 0;
        while (reloadTimer < reloadSpeed)
        {
            //while reloading, visible show the reload timer changing
            UIManager.instance.reloadMeter.value += Time.deltaTime;
            reloadTimer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
		if (UIManager.instance)
		{
            //set it back to zero so it is no longer visible
			UIManager.instance.reloadMeter.value = 0;
		}
        //reset our current magazine
        currentMagazine = magazineSize;
		updateAmmoText();
	}
}
