using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Details", menuName = "Weapons/Weapon Detail", order = 1)]
public class WeaponData : ScriptableObject {
    //we'll just attach this to the weapon prefab itself
    
    public WeaponProjectile projectile;
    public int damage;
    public int magazineSize;
    public float fireRate;
    public bool singleFire;//maybe use an enum with the different firing types
    public float projectileSpeed;
    public float reloadSpeed;
    public float spread;
    //perhaps something for spread?
    //this can be extended to different weapons
}
