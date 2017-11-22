using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MeleeBehaviour for melee weapons
/// designed to be extendable for special melee weapons
/// </summary>
public class MeleeBehaviour : MonoBehaviour {
	public MeleeData meleeData;
	protected float range;
	protected int damage;
	protected float delayBetweenAttack;//in seconds - determines how often we can attack
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
            //we use raycasts for our melee weapons
            //this is functionally the best
            //we would display an animation along with this to make it look nice
            //and provide good feedback to the player
			RaycastHit meleeHit;
			if(Physics.Raycast(transform.position, transform.forward, out meleeHit, range))
			{
				HealthManager targetHealth = meleeHit.transform.GetComponent<HealthManager>();
				if (targetHealth)
				{
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
