using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float sens;
    private float yCameraMovement;
    private float xCameraMovement;

    // Update is called once per frame
    void Update()
    {
		GameState state = GameStateManager.instance.GetCurrentGameState();
		if (state != GameState.DEAD && state != GameState.GAMEPAUSE)
		{
			xCameraMovement += sens * Input.GetAxis("Mouse X");
			yCameraMovement -= sens * Input.GetAxis("Mouse Y");
			if (yCameraMovement > 90)
			{
				yCameraMovement = 89;
			}
			if (yCameraMovement < -90)
			{
				yCameraMovement = -89;
			}
			transform.eulerAngles = new Vector3(
				Mathf.Clamp(yCameraMovement, -90, 90),
					xCameraMovement, 0);
		}
    }
}
