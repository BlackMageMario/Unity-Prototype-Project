using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PickUp/PickUpDetail")]
public class PickUpDetails : ScriptableObject {
	public PickUp ourPickup;
	public bool spawnContinuous;// we#'re not going to use ammo THEREFORE we want an option to NOT spawn an item IF it spawned once
	// Use this for initialization
}
