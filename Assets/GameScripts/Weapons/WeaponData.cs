using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple weapon data class holding information on weapon stats
/// could be potentially extended for more specialised weapons
/// </summary>
[CreateAssetMenu(fileName = "Weapon Details", menuName = "Weapons/Weapon Detail", order = 1)]
public class WeaponData : ScriptableObject {
    public WeaponProjectile projectile;//projectile associated with weapon
    public int damage;
    public int magazineSize;
    public float fireRate;
    public bool singleFire;//could replace with an enum of different firing types
    //e.g. burst fire etc.
    public float projectileSpeed;
    public float reloadSpeed;
    public float spread;//not implemented on any weapon but could be used
}
