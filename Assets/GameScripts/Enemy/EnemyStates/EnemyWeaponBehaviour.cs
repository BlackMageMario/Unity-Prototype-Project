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
    protected override void weaponAttack()
    {
        GameObject firedProjectile = pool.GetComponent<ObjectPool>().spawnObject();
        firedProjectile.transform.position = firePoint.position;
        firedProjectile.transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), GetComponentInParent<Collider>());
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
    }
}
