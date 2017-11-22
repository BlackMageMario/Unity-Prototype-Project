using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/ChargeDecision")]
/// <summary>
/// Change to the charge state if we can charge the target - see ChargeBehaviour
/// </summary>
public class ChargeDecision : Decision {
	public override bool Decide(StateManager state)
	{
		return ChDecision(state);
	}
	private bool ChDecision(StateManager state)
	{
        bool value = state.GetComponent<ChargeBehaviour>().CanChargeTarget();
        return value;
	}
}
