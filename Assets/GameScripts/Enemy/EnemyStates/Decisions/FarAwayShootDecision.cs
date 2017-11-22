using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Check if we can see the player
/// This is based partially on the notes in Lecture 7 - the section on checking if a zombie could see a player
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/FarAwayShootDecision")]
public class FarAwayShootDecision : Decision {
	public override bool Decide(StateManager state)
	{
		return CanSeeTarget(state);
	}
	private bool CanSeeTarget(StateManager state)
	{
		Vector3 stateDirection = state.transform.forward.normalized;//the direction of object (enemy) with the state manager
		Vector3 targetDirection = state.target.transform.forward.normalized;//the direction of the target
		float dotProduct = stateDirection.x * targetDirection.x + stateDirection.y * targetDirection.y + stateDirection.z * targetDirection.z;
        //dot product of the two vectors
		if(dotProduct > 0)//assuming that our shooter can look in 180 degrees, this checks if it can see the player from where it is
		{
            //this code did not entirely work but the assumption was that we still needed to send our a raycast to see if the
            //target could be found, as any object in the way would block the enemy's "sight"
			//potentially see palyer
			//cast a raycast
			RaycastHit info;
			if(Physics.Raycast(state.transform.position, stateDirection, out info, state.movementStats.engageDistance))
			{
				if(info.transform.gameObject == state.target)
				{
					return true;
				}
				return false;
			}
			return false;
		}
		return false;
	}
}
