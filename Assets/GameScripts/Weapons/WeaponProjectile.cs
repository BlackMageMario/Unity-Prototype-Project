using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour {
    private int damage;//damage from weapon data from the weapon behaviour
    private float projectileSpeed;
    private Rigidbody body;
	void Start () {
        body = GetComponent<Rigidbody>();
	}

    public void weaponStats(int weaponDamage, float weaponProjectileSpeed)
    {
        //set up projectile stats
        damage = weaponDamage;
        projectileSpeed = weaponProjectileSpeed;
    }

	
	// Update is called once per frame
	void FixedUpdate ()
    {
        body.AddForce(transform.right * projectileSpeed);
	}
    //collision detection
    //we'll use triggers for this
    //right now we'll just make them disappear if they hit anything solid
    void OnTriggerEnter(Collider other)
    {
        HealthManager healthObject = other.GetComponent<HealthManager>();
        if (healthObject)//i think this chekcs if hte object exists. I'm surprised it works
        {
            healthObject.takeDamage(damage);
        }
        Destroy(this.gameObject);
        //hit detection code
        //for now let's destroy it
        //because we need to figure out what it should detect
    }
}
