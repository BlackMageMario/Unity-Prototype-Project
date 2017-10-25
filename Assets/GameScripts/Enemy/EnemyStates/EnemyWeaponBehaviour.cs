using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehaviour : WeaponBehaviour {

    protected Transform firePoint;
	protected override void Start () {
        canFire = true;
        projectile = weaponData.projectile;
        fireRate = weaponData.fireRate;
        magazineSize = weaponData.magazineSize;
        currentMagazine = magazineSize;
        reloadSpeed = weaponData.reloadSpeed;
        spread = weaponData.spread;
        firePoint = GetComponentInChildren<Transform>();
		if (!pool)
		{
			pool = new GameObject("Pool: " + projectile.name);
			pool.AddComponent<ObjectPool>();
			pool.GetComponent<ObjectPool>().setUpPool(projectile.gameObject, defaultProjectilesToPool);
		}
	}

    public override void fireGun()
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
        }
    }
    protected override void weaponAttack()
    {
        GameObject firedProjectile = pool.GetComponent<ObjectPool>().spawnObject();
        firedProjectile.transform.position = firePoint.position;
        firedProjectile.transform.rotation = firePoint.rotation * Quaternion.Euler(0, -90, 0);
        projectile.GetComponent<Rigidbody>().velocity += GetComponentInParent<Rigidbody>().velocity;
        firedProjectile.GetComponent<WeaponProjectile>().weaponStats(weaponData.damage, weaponData.projectileSpeed);
        currentMagazine -= 1;//take away a bullet
    }
    protected override IEnumerator reloadMagazine()
    {
        //return base.reloadMagazine();
        Debug.Log("Reloading");
        canFire = false;
        yield return new WaitForSeconds(reloadSpeed);
        Debug.Log("Finished");
        currentMagazine = magazineSize;
        canFire = true;
    }
}
