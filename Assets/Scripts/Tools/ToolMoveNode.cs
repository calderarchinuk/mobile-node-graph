using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolMoveNode : ToolBase
{
	public override void EnableTool ()
	{
		base.EnableTool ();
		InputManager.OnInputDown += InputManager_OnInputDown;
		InputManager.OnInputUp += InputManager_OnInputUp;
	}

	CommonNode selectedNode;
	Vector2 originalPos;

	void InputManager_OnInputDown (CommonNode node, bool ignored, Vector2 pos)
	{
		selectedNode = null;
		if (node == null){return;}
		selectedNode = node;
	}

	void InputManager_OnInputUp (CommonNode node, bool hasMoved, Vector2 pos)
	{
		if (node != null){return;}
		if (!hasMoved){return;}
		if (selectedNode == null){return;}



		if (GameInstance.NodeCanvas.SelectedNode == selectedNode)
		{

		}
		else
		{
			selectedNode.data.Position = pos;
			selectedNode.transform.position = selectedNode.data.Position;
			NodeCanvas.MovedNodeEvent(selectedNode);
			selectedNode = null;
		}
	}

	public override void DisableTool ()
	{
		base.DisableTool ();
		InputManager.OnInputDown -= InputManager_OnInputDown;
		InputManager.OnInputUp -= InputManager_OnInputUp;
	}
}
