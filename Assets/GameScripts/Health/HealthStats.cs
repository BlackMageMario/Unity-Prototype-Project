using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple scriptable object that contains health values and the methods used to manipulate them
/// using a pattern of passing in the current health, current armour, and the damage
/// these methods are statics
/// </summary>
[CreateAssetMenu(fileName = "Health Statistics", menuName = "Health/Health Stats", order = 1)]
public class HealthStats : ScriptableObject {
    public int maxHealth;//the max amount of health the object can have
    public int maxArmour;// max amount of armour
    public int spawnHealth;// the number of health the object has on spawn
    public int spawnArmour;// spawn armour

    //delegate pattern
    public static int reduceHealth(int currentHealth, int damage)
    {
        return currentHealth -= damage;
    }
    //for objects with armour
    public static int armourReduceHealth(int currentHealth, int currentArmour, int damage)
    {
        return currentHealth -= damage - (1 / 3 * currentArmour);
    }
    //possibly a better design could have been found - perhaps pass in a reference to the object with health?
    public static int reduceArmour(int currentArmour, int damage)
    {
        int newArmour = currentArmour -= damage * 1 / 3;
        return newArmour < 0 ? newArmour : 0;
    }
    //increase the current health of the object
    public static int increaseHealth(int currentHealth, int maxHealth, int heal)
    {
        int newHealth = currentHealth + heal;
        return newHealth > maxHealth ? maxHealth : newHealth;
    }
    //increase the current armour of the object
    public static int increaseArmour(int currentArmour, int maxArmour, int armourHeal)
    {
        int newArmour = currentArmour + armourHeal;
        return armourHeal > maxArmour ? maxArmour : newArmour;
    }

}
