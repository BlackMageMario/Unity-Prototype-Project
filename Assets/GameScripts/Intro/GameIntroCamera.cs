using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntroCamera : MonoBehaviour {

    public float maxYAngle;
    public float minYAngle;
    private float yMovement = 5;
    // Use this for initialization
    private bool moveTowardsMin = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moveTowardsMin)
        {
            transform.Rotate(Vector3.up, yMovement * Time.deltaTime);
            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 5 * Time.deltaTime, transform.rotation.z, transform.rotation.w);
        }
        else
        {
            transform.Rotate(Vector3.up, -yMovement * Time.deltaTime);
        }
        if (transform.rotation.y > Mathf.Deg2Rad * maxYAngle)
        {
            moveTowardsMin = false;
        }
        if(transform.rotation.y < Mathf.Deg2Rad * minYAngle)
        {
            moveTowardsMin = true;
        }

	}
}
