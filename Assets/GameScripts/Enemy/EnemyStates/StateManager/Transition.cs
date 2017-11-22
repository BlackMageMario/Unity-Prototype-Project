using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implementation of Transitions - a simple C# class - it could also be a struct for further efficency
/// Modified and adapted from Pluggable AI tutorial from Unity - https://unity3d.com/learn/live-training/session/pluggable-ai-scriptable-objects
/// </summary>
[System.Serializable]
public class Transition {
	public Decision decision;
	public State trueState;
	public State falseState;
}
