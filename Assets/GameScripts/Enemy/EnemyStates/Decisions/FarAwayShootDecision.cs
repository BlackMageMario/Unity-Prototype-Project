using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/FarAwayShootDecision")]
public class FarAwayShootDecision : Decision {
	public override bool Decide(StateManager state)
	{
		return CanSeeTarget(state);
	}
	private bool CanSeeTarget(StateManager state)
	{
		Vector3 stateDirection = state.transform.forward.normalized;
		Vector3 targetDirection = state.target.transform.forward.normalized;
		float dotProduct = stateDirection.x * targetDirection.x + stateDirection.y * targetDirection.y + stateDirection.z * targetDirection.z;
		if(dotProduct > 0)
		{
			//potentially see palyer
			//cast a raycast
			RaycastHit info;
			/*if(Physics.Raycast(state.transform.position, stateDirection, out info, state.movementStats.engageDistance))
			{
				if(info.transform.gameObject == state.target)
				{
					return true;
				}
				return true;
			}*/
			return true;
		}
		Debug.Log("Sending false");
		return false;
	}
}
