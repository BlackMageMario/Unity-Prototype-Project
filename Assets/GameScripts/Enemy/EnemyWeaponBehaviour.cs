using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehaviour : WeaponBehaviour {

	protected override void Start () {
		source = gameObject.AddComponent<AudioSource>();
		source.maxDistance = 10f;
		source.spatialBlend = 1f;
		source.rolloffMode = AudioRolloffMode.Custom;
		source.volume = 1f;
		source.clip = soundForShot;
		source.playOnAwake = false;
		canFire = true;
        projectile = weaponData.projectile;
        fireRate = weaponData.fireRate;
        magazineSize = weaponData.magazineSize;
        currentMagazine = magazineSize;
        reloadSpeed = weaponData.reloadSpeed;
        spread = weaponData.spread;
		pool = ObjectPool.getPool(projectile.gameObject, defaultProjectilesToPool);
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
		Debug.Log("Enemy enable");
	}
	protected override void OnEnable()
	{
		Debug.Log("Enemy disable");
	}
	protected override void weaponAttack()
    {
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
        //return base.reloadMagazine();
        canFire = false;
        yield return new WaitForSeconds(reloadSpeed);
        currentMagazine = magazineSize;
    }
}
