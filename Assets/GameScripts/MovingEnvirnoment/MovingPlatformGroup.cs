using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformGroup : MonoBehaviour {
    public GameObject[] platforms;
    // Use this for initialization
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }
    public void movePlatforms(float minY, float maxY)
    {
        //let's move them
        for(int i = 0; i < platforms.Length; i++)
        {
            float yGoal = Random.Range(minY, maxY);
            Debug.Log("Our goal: " + yGoal);
            platforms[i].GetComponent<Rigidbody>().MovePosition(new Vector3(platforms[i].transform.position.x, yGoal, platforms[i].transform.position.z));
        }
        //our platforms will need a rigidbody
    }

}
