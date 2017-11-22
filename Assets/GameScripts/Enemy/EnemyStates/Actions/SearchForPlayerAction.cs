using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Search for our player - we teleport to a random location in an attempt to find the player
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Actions/SearchForPlayerAction")]
public class SearchForPlayerAction : Action
{
	public override void Act(StateManager controller)
	{
		Random.InitState((int)System.DateTime.Now.Ticks);
		SearchForPlayer(controller);
	}
	private void SearchForPlayer(StateManager controller)
	{
		//teleporting is implemented in TeleportBehaviour
		controller.GetComponent<TeleportBehaviour>().Teleport();
	}
}
