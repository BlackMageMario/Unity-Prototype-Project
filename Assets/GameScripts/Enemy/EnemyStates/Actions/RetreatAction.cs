using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Retreat")]
///
/// Retreat action - unfortunately, this was not implemented in the end
/// but this action if implemented would attempt to run away from the target
/// by finding a reachable location as far as possible from the target
///
public class RetreatAction : Action {
	public override void Act(StateManager manager)
	{
		Retreat(manager);
	}
	private void Retreat(StateManager manager)
	{
		//unimplemented
	}
}
