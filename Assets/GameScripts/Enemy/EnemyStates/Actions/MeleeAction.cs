using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/MeleeAction")]
/// <summary>
/// Melee action - check if in range, and then do the melee action
/// </summary>
public class MeleeAction : Action {

	public override void Act(StateManager controller)
	{
		meleeAttack(controller);
	}

	private void meleeAttack(StateManager controller)
	{
		MeleeBehaviour meleeAttack = controller.GetComponent<MeleeBehaviour>();
		//check if in range
		if(Vector3.Distance(controller.transform.position, 
			controller.target.transform.position) < meleeAttack.meleeData.range)
		{
			//if in range, execute
            //implementation details left in MeleeBehaviour
			meleeAttack.swingMelee();
		}
	}
}
