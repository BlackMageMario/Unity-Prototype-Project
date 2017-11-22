using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ChargeAction")]
/// <summary>
/// Do the charge attack if we can. See charge behaviour
/// </summary>
public class ChargeAction : Action
{
	public override void Act(StateManager controller)
	{
		ChargeTarget(controller);
	}
	private void ChargeTarget(StateManager controller)
	{
        controller.GetComponent<ChargeBehaviour>().DoChargeAttack();
	}
}
