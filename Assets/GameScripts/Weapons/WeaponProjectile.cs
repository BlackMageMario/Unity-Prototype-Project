using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Weapon Projectile class - used for all projectiles
/// Extendable for special types of projectiles
/// </summary>
public class WeaponProjectile : MonoBehaviour {
    protected int damage;//damage from weapon data from the weapon behaviour
    protected float projectileSpeed;//our fast our projectile should go
	private int disappearDistance = 2000;//at what distance do we want the object to disappear
    //because it'll no longer be useful and just cause performance
    //issues
	private Rigidbody body;//associated rigidbody of projectile
	protected virtual void Start ()
    {
        body = GetComponent<Rigidbody>();
		StartCoroutine(disappearBullet());
	}
	protected virtual void OnDisable()
	{
        //resets position etc.
        //needs to be done in on disable - ensure that every
        //projectile has this
        body = GetComponent<Rigidbody>();//get body again
        body.velocity = new Vector3(0, 0, 0);
		body.angularVelocity = new Vector3(0, 0, 0);
		body.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		StopAllCoroutines();
	}
	protected virtual void OnEnable()
	{
		StartCoroutine(disappearBullet());
	}
	public virtual void weaponStats(int weaponDamage, float weaponProjectileSpeed)
    {
        damage = weaponDamage;
        projectileSpeed = weaponProjectileSpeed;
	}
	protected virtual void FixedUpdate ()
    {
		GameState state = GameStateManager.instance.GetCurrentGameState();
		if (state != GameState.GAMEPAUSE && state != GameState.DEAD)
		{
            //we don't want these projectiles to move if the gamestate is set to dead
			body.AddForce(transform.right * projectileSpeed);
		}
	}
    //collision detection
    //we'll use triggers for this
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<WeaponProjectile>() && other.tag != "Triggers")
        {
            //as long as the object isn't another projectile or a trigger
            //it can't destroy it
            HealthManager healthObject = other.GetComponent<HealthManager>();
            if (healthObject)
            {
                healthObject.takeDamage(damage);
            }
			GetComponent<PooledObject>().pool.ReturnObject(this.gameObject);
        }
    }
	protected virtual IEnumerator disappearBullet()
	{
		//the bullet has gone past a certain distance
		//return it
		Vector3 originalPosition = transform.position;//position at time this func is called
		while(Vector3.Distance(originalPosition, transform.position) < disappearDistance)
		{
			yield return new WaitForFixedUpdate();
		}
		GetComponent<PooledObject>().pool.ReturnObject(this.gameObject);
	}
}
