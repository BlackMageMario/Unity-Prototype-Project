using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour {
    private int damage;//damage from weapon data from the weapon behaviour
    private float projectileSpeed;
	private int disappearDistance = 2000;
	private Rigidbody body;
	protected virtual void Start () {
        body = GetComponent<Rigidbody>();
		StartCoroutine(disappearBullet());
		//Debug.Log("Position: " + transform.position);
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
        //set up projectile stats
        damage = weaponDamage;
        projectileSpeed = weaponProjectileSpeed;
		///Debug.Log("Initial Position: " + transform.position);
	}
	// Update is called once per frame
	protected virtual void FixedUpdate ()
    {
        body.AddForce(transform.right * projectileSpeed);
	}
    //collision detection
    //we'll use triggers for this
    //right now we'll just make them disappear if they hit anything solid
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<WeaponProjectile>())
        {
            //as long as the object isn't another projectile it can't
            //destroy it
            //
            Debug.Log(other.name);
            HealthManager healthObject = other.GetComponent<HealthManager>();
            if (healthObject)//i think this chekcs if hte object exists. I'm surprised it works
            {
                Debug.Log("Health Object: " + healthObject);
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
			Debug.Log("Active");
			yield return new WaitForFixedUpdate();
		}
		//once while is done
		Debug.Log("Reached here");
		GetComponent<PooledObject>().pool.ReturnObject(this.gameObject);
	}
}
