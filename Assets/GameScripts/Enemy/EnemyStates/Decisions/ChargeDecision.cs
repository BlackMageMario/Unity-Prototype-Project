using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDecision : Decision {
	public float chargeRange;//the range at which we can start charging
	public override bool Decide(StateManager state)
	{
		return ChDecision(state);
	}
	private bool ChDecision(StateManager state)
	{
		//check whether the distance is smaller than the range
		return Vector3.Distance(state.transform.position, state.target.transform.position) <= chargeRange ? true : false;
	}
}
