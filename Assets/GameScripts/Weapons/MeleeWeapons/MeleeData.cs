using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple scriptable object for the data for melee weapons
/// </summary>
[CreateAssetMenu(menuName ="MeleeWeapon/MeleeWeaponData")]
public class MeleeData : ScriptableObject {
	public float range;//range of the raycast created by the weapon animation
					   //much simplier and more easy to understand than using models for this
					   //damage is dealt instantly and done over one frame
	public int damage;
	public float delayBetweenAttack;
}
