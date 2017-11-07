using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/ChargeDecision")]
public class ChargeDecision : Decision {
	public override bool Decide(StateManager state)
	{
		return ChDecision(state);
	}
	private bool ChDecision(StateManager state)
	{
        //check whether the distance is smaller than the range
        bool value = state.GetComponent<ChargeBehaviour>().CanChargeTarget();
        //Debug.Log("Beginning Charge? " + value);
        return value;
	}
}
