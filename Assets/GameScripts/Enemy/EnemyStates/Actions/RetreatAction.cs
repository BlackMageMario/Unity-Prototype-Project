using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Retreat")]
public class RetreatAction : Action {
	public override void Act(StateManager manager)
	{
		Retreat(manager);
	}
	private void Retreat(StateManager manager)
	{
		//we need to retreat until we can't see the target
		//so our enemy needs to be able to see where the nearest wall is and run towards that
		//we need pathing which is kind of a pain in the ass to create... i might do this later then
	}
}
