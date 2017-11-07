using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/FinishChargeDecision")]
public class FinishChargeDecision : Decision {
    public override bool Decide(StateManager state)
    {
        return ChargeFinished(state);
    }
    private bool ChargeFinished(StateManager state)
    {

        bool value = state.GetComponent<ChargeBehaviour>().isChargeFinished();
        //Debug.Log("Charged finished? " + value);
        return value;
    }

}
