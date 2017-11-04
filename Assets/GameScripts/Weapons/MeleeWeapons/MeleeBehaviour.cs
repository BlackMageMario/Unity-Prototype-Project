using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : MonoBehaviour {
	public MeleeData meleeData;//what are we going to have in our melee data?
							   // Use this for initialization
	protected float range;
	protected int damage;
	protected float delayBetweenAttack;//in seconds
	protected bool canAttack;
	protected virtual void Start () {
		canAttack = true;
		range = meleeData.range;
		damage = meleeData.damage;
		delayBetweenAttack = meleeData.delayBetweenAttack;
	}
	
	public virtual void swingMelee()
	{
		if(canAttack)
		{
			RaycastHit meleeHit;
			if(Physics.Raycast(transform.position, transform.forward, out meleeHit, range))
			{
				HealthManager targetHealth = meleeHit.transform.GetComponent<HealthManager>();
				if (targetHealth)
				{
					//valid target (for now)
					targetHealth.takeDamage(damage);
				}
			}
			StartCoroutine(waitForNextAttack());
		}
	}
	protected virtual IEnumerator waitForNextAttack()
	{
		canAttack = false;
		yield return new WaitForSeconds(delayBetweenAttack);
		canAttack = true;
	}
}
