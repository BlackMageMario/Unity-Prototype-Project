using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Based upon WeaponBehaviour - this is largely similiar for projectile weapons
/// HOWEVER it does not use player specific implementation details
/// On reflection, a better design would have been to create an abstract
/// WeaponBehaviour class or an interface
/// Define the behaviour needed for both weapons in general there
/// and then define the specifics needed in both in extended classes
/// </summary>
public class EnemyWeaponBehaviour : WeaponBehaviour {

	protected override void Start () {
		source = gameObject.AddComponent<AudioSource>();
		source.maxDistance = 10f;
		source.spatialBlend = 1f;
		source.rolloffMode = AudioRolloffMode.Custom;
		source.volume = 1f;
		source.clip = soundForShot;
		source.playOnAwake = false;
		
        projectile = weaponData.projectile;
        fireRate = weaponData.fireRate;
        magazineSize = weaponData.magazineSize;
        currentMagazine = magazineSize;
        reloadSpeed = weaponData.reloadSpeed;
        spread = weaponData.spread;
		pool = ObjectPool.getPool(projectile.gameObject, defaultProjectilesToPool);
        canFire = true;
    }

    public override void fireGun()
    {
        if (canFire)
        {
            if (currentMagazine > 0)//this check *might* be unnecessary
            {
                weaponAttack();
                StartCoroutine(canFireWeapon());
            }
        }
    }
	protected override void OnDisable()
	{
        //currently we override onDisable and onEnable just to avoid
        //player specific implementation details
        //however this could e useful for something else
	}
	protected override void OnEnable()
	{

	}
	protected override void weaponAttack()
    {
        //very similiar to default implementation
        GameObject firedProjectile = pool.GetComponent<ObjectPool>().spawnObject();
        firedProjectile.transform.position = firePoint.transform.position;
        firedProjectile.transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), GetComponentInParent<Collider>());
        projectile.GetComponent<Rigidbody>().velocity += GetComponentInParent<Rigidbody>().velocity;
        firedProjectile.GetComponent<WeaponProjectile>().weaponStats(weaponData.damage, weaponData.projectileSpeed);
		source.Play();
		currentMagazine -= 1;//take away a bullet
    }
    protected override IEnumerator reloadMagazine()
    {
        //we don't need to update any meters during this
        canFire = false;
        yield return new WaitForSeconds(reloadSpeed);
        currentMagazine = magazineSize;
    }
}
