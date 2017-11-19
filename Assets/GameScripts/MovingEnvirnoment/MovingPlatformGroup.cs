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
			StartCoroutine(executionOfPlatforms(minY, maxY, platforms[i].GetComponent<Rigidbody>()));
        }
        //our platforms will need a rigidbody
    }
	IEnumerator executionOfPlatforms(float minY, float maxY, Rigidbody toMove)
	{
		float yGoal = Random.Range(minY, maxY);
		Vector3 targetPosition = new Vector3(toMove.position.x, yGoal, toMove.position.z);
		
		while(Vector3.Distance(targetPosition, toMove.position) >= 0.05)
		{
			Vector3 direction = (targetPosition - toMove.position).normalized;
			toMove.MovePosition(toMove.position + direction * 2 * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}
	}
}
