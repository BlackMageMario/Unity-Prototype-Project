using System.Collections;
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
}
