using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/MeleeAction")]
public class MeleeAction : Action {

	public override void Act(StateManager controller)
	{
		//throw new System.NotImplementedException();
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
			meleeAttack.swingMelee();
		}
	}
}
