using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectileScript : WeaponProjectile {
	public GameObject explosionEffect;
	protected override void OnTriggerEnter(Collider other)
	{
		if(!other.GetComponent<WeaponBehaviour>() && other.tag != "Triggers")
		{
			//create explosion sphere
			GameObject explosion = Instantiate(explosionEffect, transform);
			explosion.AddComponent<RocketExplosion>();
			GetComponent<PooledObject>().pool.ReturnObject(this.gameObject);
		}
	}
	private class RocketExplosion : MonoBehaviour
	{
		private int damage;
		public void InitExplosion(int damage)
		{
			this.damage = damage;
			StartCoroutine(removeExpolsion());
		}
		private void OnTriggerEnter(Collider other)
		{
			HealthManager healthObject = other.GetComponent<HealthManager>();
			if (healthObject)
			{
				Debug.Log("Damage takers: " + healthObject);
				healthObject.takeDamage(damage);
				healthObject.gameObject.GetComponent<Rigidbody>()
					.AddExplosionForce(2000,
						transform.position + healthObject.transform.position, 40);
			}
			
		}
		IEnumerator removeExpolsion()
		{
			yield return new WaitForSeconds(2f);
			Destroy(this.gameObject);
		}
	}
}

