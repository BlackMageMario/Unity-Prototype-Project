using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRayCast : MonoBehaviour {

	
	void Start () {
		StartCoroutine(destroyBeam());
	}
	
	// Update is called once per frame
	/*void FixedUpdate () {
		
	}*/
	IEnumerator destroyBeam()
	{
		yield return new WaitForSeconds(2f);
		Destroy(this.gameObject);
	}
}
