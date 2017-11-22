using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
/* https://answers.unity.com/questions/1125768/how-do-i-predict-the-position-of-my-player-for-the.html helped with prediction
 * Shoot at the target 
 *
 */
public class ShootAction : Action {

    public override void Act(StateManager manager)
    {
        Shoot(manager);
    }
    private void Shoot(StateManager manager)
    {
		Transform aiPos = manager.transform;//need to cache the target and the enemy's position
		Transform targetPos = manager.target.transform;
		Vector3 targetVel = manager.target.GetComponent<Rigidbody>().velocity;// get the velocity of the target to help will prediction
		float projectileSpeed = manager.GetComponentInChildren<WeaponBehaviour>().weaponData.projectileSpeed;//get the speed of our weapon to help with prediction
		if (Vector3.Distance(aiPos.position, targetPos.position) < manager.movementStats.engageDistance)
        {
			//need to predict where the target will be - get displacement
			Vector3 displacement = manager.transform.position 
				- manager.target.transform.position;
			float targetAngle = Vector3.Angle(-displacement, targetVel);
			//check if the target is standing still or if the projectile cannot hit the target
			if(targetVel.magnitude ==0 || (targetVel.magnitude > projectileSpeed 
				&& Mathf.Sin(targetAngle) / projectileSpeed 
					> Mathf.Cos(targetAngle /targetVel.magnitude)))
			{
                //just look directly at their postion and try your best
				aiPos.LookAt(manager.target.transform.position);
			}
            else
			{
                //get the shoot point using angle maths - adjust where to shoot based on our velocity
				float angleToShoot = Mathf.Asin(Mathf.Sin(targetAngle) * targetVel.magnitude / projectileSpeed);
				Vector3 shootPoint = targetPos.position +
					(targetVel * displacement.magnitude / Mathf.Sin(Mathf.PI - targetAngle - angleToShoot) *
					Mathf.Sin(angleToShoot) / targetVel.magnitude);
				aiPos.LookAt(shootPoint);
			}
            //now shoot the gun
            manager.GetComponentInChildren<WeaponBehaviour>().fireGun();
        }
    }
}
