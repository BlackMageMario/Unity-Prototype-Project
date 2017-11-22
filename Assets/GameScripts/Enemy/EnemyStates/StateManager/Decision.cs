using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Our abstraction decision class - our actual decisions implement this
/// Modified and adapted from Pluggable AI tutorial from Unity - https://unity3d.com/learn/live-training/session/pluggable-ai-scriptable-objects
/// </summary>
public abstract class Decision : ScriptableObject 
{
	public abstract bool Decide(StateManager state);
}
