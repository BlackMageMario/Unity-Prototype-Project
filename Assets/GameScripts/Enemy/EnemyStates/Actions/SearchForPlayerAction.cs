using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
		//new idea = need new script
		controller.GetComponent<TeleportBehaviour>().Teleport();
	}
}
