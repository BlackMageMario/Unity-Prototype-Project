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

    public static UIManager instance = null;
    public Text ammoText;
    public Slider reloadMeter;
    public Slider healthMeter;
    public Slider armourMeter;
    void Awake()
    {
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
