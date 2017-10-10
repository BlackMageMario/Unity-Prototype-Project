using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Health Statistics", menuName = "Health/Health Stats", order = 1)]
public class HealthStats : ScriptableObject {
    public int maxHealth;
    public int maxOverheal;
    public int maxArmour;//it might be best to refactor this to include things like armour and spawn health for the player only
    public int spawnHealth;
    public int spawnArmour;
    //delegate pattern - don't want scriptable object holding weirdness
    public static int reduceHealth(int currentHealth, int damage)
    {
        //for normal enemies without armour
        return currentHealth -= damage;
    }
    public static int armourReduceHealth(int currentHealth, int currentArmour, int damage)
    {
        return currentHealth -= damage - (1 / 3 * currentArmour);
    }
    public static int reduceArmour(int currentArmour, int damage)
    {
        int newArmour = currentArmour -= damage * 1 / 3;
        return newArmour < 0 ? newArmour : 0;
    }
    public static int increaseHealth(int currentHealth, int maxHealth, int heal)
    {
        int newHealth = currentHealth + heal;
        return newHealth > maxHealth ? maxHealth : newHealth;
    }
    public static int increaseArmour(int currentArmour, int maxArmour, int armourHeal)
    {
        int newArmour = currentArmour + armourHeal;
        return armourHeal > maxArmour ? maxArmour : newArmour;
    }

}
