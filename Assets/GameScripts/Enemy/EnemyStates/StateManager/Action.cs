using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implementation of abstract Actions - our actual actions implement this
/// Modified and adapted from Pluggable AI tutorial from Unity - https://unity3d.com/learn/live-training/session/pluggable-ai-scriptable-objects
/// </summary>
public abstract class Action : ScriptableObject {
    public abstract void Act(StateManager controller);
}
