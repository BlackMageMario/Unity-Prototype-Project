using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Retreat decision - we decide whether to retreat based on whether the gun is currently loaded or not
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/RetreatDecision")]
public class RetreatDecision : Decision {
	public override bool Decide(StateManager state)
	{
		bool isReloading = checkGun(state);
		return isReloading;
	}

	private bool checkGun(StateManager state)
	{
        //check the magazine - if its empty, return true
		return state.GetComponentInChildren<WeaponBehaviour>().getCurrentMagazine() <= 0 ? true : false;
	}
}
