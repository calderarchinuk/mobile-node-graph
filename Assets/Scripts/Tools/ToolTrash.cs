using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTrash : ToolBase
{
	public override void EnableTool ()
	{
		base.EnableTool ();
		InputManager.OnInputDown += InputManager_OnInputDown;
		InputManager.OnTrashNode += InputManager_OnTrashNode;
	}

	void InputManager_OnTrashNode ()
	{
		if (selectedNode != null)
		{
			Destroy(selectedNode.gameObject);
			NodeCanvas.DeleteNodeEvent(selectedNode);
		}
		selectedNode = null;
	}

	CommonNode selectedNode;
	Vector2 originalPos;

	void InputManager_OnInputDown (CommonNode node, bool ignored, Vector2 pos)
	{
		selectedNode = null;
		if (node == null){return;}
		selectedNode = node;
	}

	public override void DisableTool ()
	{
		base.DisableTool ();
		InputManager.OnInputDown -= InputManager_OnInputDown;
		InputManager.OnTrashNode -= InputManager_OnTrashNode;
	}
}
