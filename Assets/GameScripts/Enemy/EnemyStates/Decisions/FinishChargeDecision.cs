using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/FinishChargeDecision")]
///<summary>
/// return the value of isChargedFinished() - important to see if we should change out of the ChargeState
///</summary>
public class FinishChargeDecision : Decision {
    public override bool Decide(StateManager state)
    {
        return ChargeFinished(state);
    }
    private bool ChargeFinished(StateManager state)
    {
        bool value = state.GetComponent<ChargeBehaviour>().isChargeFinished();
        return value;
    }

}
