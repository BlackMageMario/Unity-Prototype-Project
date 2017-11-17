using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformManager : MonoBehaviour {
    public GameObject[] platformGroups;
    public float maxY;
    public float minY;
    //maybe this should be generalised I'm not sure
    //not for this game i suppose
	// Use this for initialization
	void Start () {
        StartCoroutine(testFunction());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void callGroups()
    {
        for (int i = 0; i < platformGroups.Length; i++)
        {
           platformGroups[i].GetComponent<MovingPlatformGroup>().movePlatforms(minY, maxY);
        }
    }
    IEnumerator testFunction()
    {
        while (true)
        {
            callGroups();
            yield return new WaitForSeconds(2f);
        }

        
    }
}
