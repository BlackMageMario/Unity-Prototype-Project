using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {
    //from unity tutorials "Live Session: Pluggable AI with ScriptableObjects
    public abstract void Act(StateManager controller);
}
