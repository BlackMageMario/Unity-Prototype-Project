﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ChargeAction")]
public class ChargeAction : Action
{
	//public float ChargeDuration;
	public float chargeForceX;
	//this is a more one off action
	public override void Act(StateManager controller)
	{
		ChargeTarget(controller);
	}
	private void ChargeTarget(StateManager controller)
	{
        controller.GetComponent<ChargeBehaviour>().DoChargeAttack();
	}
	/*private IEnumerator Charge(Vector3 chargePoint)
	{
		float elapsedTime = 0;
		while(elapsedTime < ChargeDuration)
		{
			yield return new WaitForFixedUpdate();
			elapsedTime += Time.fixedDeltaTime;
		}
		
	}*/
}
