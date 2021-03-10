using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolConnectNode : ToolBase
{
	public override void EnableTool ()
	{
		base.EnableTool ();
		InputManager.OnInputDown += InputManager_OnInputDown;
		InputManager.OnInputUp += InputManager_OnInputUp;
	}

	CommonNode startNode;
	Vector2 originalPos;

	void InputManager_OnInputDown (CommonNode node, bool ignored, Vector2 pos)
	{
		startNode = null;
		if (node == null){return;}
		startNode = node;
	}

	void InputManager_OnInputUp (CommonNode endNode, bool hasMoved, Vector2 pos)
	{
		if (endNode == null){return;}
		if (startNode == null){return;}
		if (!hasMoved){return;}

		GameInstance.NodeCanvas.ToggleConnection(startNode,endNode);
		Debug.Log("toggle connection " + startNode.data.Name + "  " + endNode.data.Name);

		startNode = null;
	}

	public override void DisableTool ()
	{
		base.DisableTool ();
		InputManager.OnInputDown -= InputManager_OnInputDown;
		InputManager.OnInputUp -= InputManager_OnInputUp;
	}
}
