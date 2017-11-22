using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple scriptable object that contains movement data for our enemies
/// </summary>
[CreateAssetMenu(fileName = "EnemyMovementStats", menuName = "Enemy/Enemy Movement Stats", order = 1)]
public class EnemyMovementStats : ScriptableObject {
    public float forwardSpeed;//forward movementspeed
    public float backwardSpeed;//backwards movementspeed
    public float sidewaySpeed;//sideways jump speed
    public float jumpForce;//jump force
    public float engageDistance;//distance to engage at
}
