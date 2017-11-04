using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="MeleeWeapon/MeleeWeaponData")]
public class MeleeData : ScriptableObject {
	public float range;//range of the raycast created by the weapon animation
					   //much simplier and more easy to understand than using models for this
					   //damage is dealt instantly and done over one frame
					   //i might adjust this later
	public int damage;
	public float delayBetweenAttack;
}
