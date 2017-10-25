using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour {
    private int damage;//damage from weapon data from the weapon behaviour
    private float projectileSpeed;
    private Rigidbody body;
	protected virtual void Start () {
        body = GetComponent<Rigidbody>();
		StartCoroutine(projectileReturn());
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
	}

	public virtual void weaponStats(int weaponDamage, float weaponProjectileSpeed)
    {
        //set up projectile stats
        damage = weaponDamage;
        projectileSpeed = weaponProjectileSpeed;
		///Debug.Log("Initial Position: " + transform.position);
	}
	protected virtual IEnumerator projectileReturn()
	{
		yield return new WaitForSeconds(3f);//after ten seconds destroy this item
		GetComponent<PooledObject>().pool.ReturnObject(this.gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
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
            //Destroy(this.gameObject);
        }
    }
}
