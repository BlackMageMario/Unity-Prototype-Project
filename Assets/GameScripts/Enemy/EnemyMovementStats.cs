using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyMovementStats", menuName = "Enemy/Enemy Movement Stats", order = 1)]
public class EnemyMovementStats : ScriptableObject {
    public float forwardSpeed;
    public float backwardSpeed;
    public float sidewaySpeed;
    public float jumpForce;
    public float engageDistance;//distance to engage at
    //we'll need to figure out how movement works but this will work for now;
    //maybe need a target field?
}
