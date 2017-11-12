using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInstruction : MonoBehaviour {

	public GameObject instructionPanel;
	public void togglePanel()
	{
		instructionPanel.SetActive(!instructionPanel.activeSelf);
	}
}
