using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ShootAction")]
/* https://answers.unity.com/questions/1125768/how-do-i-predict-the-position-of-my-player-for-the.html helped with prediction
 * 
 *
 */
public class ShootAction : Action {

    public override void Act(StateManager manager)
    {
        Shoot(manager);
    }
    private void Shoot(StateManager manager)
    {
		Transform aiPos = manager.transform;
		Transform targetPos = manager.target.transform;
		Vector3 targetVel = manager.target.GetComponent<Rigidbody>().velocity;
		float projectileSpeed = manager.GetComponentInChildren<WeaponBehaviour>().weaponData.projectileSpeed;
		if (Vector3.Distance(aiPos.position, targetPos.position) < manager.movementStats.engageDistance)
        {
			//need to predict where the enemy will be
			Vector3 displacement = manager.transform.position 
				- manager.target.transform.position;
			float targetAngle = Vector3.Angle(-displacement, targetVel);
			//check if the target is standing still or if the projectile cannot hit the
			//target
			if(targetVel.magnitude ==0 || (targetVel.magnitude > projectileSpeed 
				&& Mathf.Sin(targetAngle) / projectileSpeed 
					> Mathf.Cos(targetAngle /targetVel.magnitude)))
			{
				aiPos.LookAt(manager.target.transform.position);
			}
            else
			{
				Debug.Log("shooting here");
				float angleToShoot = Mathf.Asin(Mathf.Sin(targetAngle) * targetVel.magnitude / projectileSpeed);
				Vector3 shootPoint = targetPos.position +
					(targetVel * displacement.magnitude / Mathf.Sin(Mathf.PI - targetAngle - angleToShoot) *
					Mathf.Sin(angleToShoot) / targetVel.magnitude);
				aiPos.LookAt(shootPoint);
			}
            manager.GetComponentInChildren<WeaponBehaviour>().fireGun();
        }
    }

}
