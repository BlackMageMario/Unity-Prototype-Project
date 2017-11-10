using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Text ammoText;
	public Text waveAnnounceText;
	public Text waveTrackText;
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

    //public static Text message;//center message text
    //maybe i should just create UI elements using prefabs instead when
    //needed??
}
