using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/RetreatDecision")]
public class RetreatDecision : Decision {
	public override bool Decide(StateManager state)
	{
		bool isReloading = checkGun(state);
		return isReloading;
		//throw new System.NotImplementedException();
	}

	private bool checkGun(StateManager state)
	{
		return state.GetComponentInChildren<WeaponBehaviour>().getCurrentMagazine() <= 0 ? true : false;
	}
}
