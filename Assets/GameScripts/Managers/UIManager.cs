﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Found from: https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
/// Modified to be a UI Manager that uses singleton pattern
/// to get global UI elements
/// </summary>
public class UIManager : MonoBehaviour
{
    //TODO: Massive refactor
    //refactor this and break all the scripts that rely on it
    //so that the code won't be horrifying by the end
    public static UIManager instance = null;
    public TextMeshProUGUI ammoText;
	public TextMeshProUGUI waveAnnounceText;
	public TextMeshProUGUI waveTrackText;
    [SerializeField]private TextMeshProUGUI pickUpWeaponPopUp;
    public Slider reloadMeter;
    public Slider healthMeter;
    public Slider armourMeter;
    public Canvas GameStartUI;
    public Canvas GameRunningUI;
    public Canvas GamePauseUI;
    public Canvas GameDeadUI;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!instance)//if this doesn't exist
        {
            instance = this;//this is our singleton
        }
        else
        {
            Destroy(this.gameObject);//destroy; extra singleton created
        }
    }
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			GameState state = GameStateManager.instance.GetCurrentGameState();
			if(state == GameState.GAMEALIVE)
			{
				GameStateManager.instance.PauseGame();
			}
			else if(state == GameState.GAMEPAUSE)
			{
				GameStateManager.instance.UnPauseGame();
			}
		}
	}
    public void generateWeaponPopUp(string weaponName)
    {
        pickUpWeaponPopUp.text = "Press E to pick up " + weaponName + ".";
    }
    public void clearWeaponPopUp()
    {
        pickUpWeaponPopUp.text = "";
    }
	//public static Text message;//center message text
	//maybe i should just create UI elements using prefabs instead when
	//needed??
}
