using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolEdit : ToolBase
{
	public override void EnableTool ()
	{
		base.EnableTool ();
		InputManager.OnInputDown += InputManager_OnInputDown;
		InputManager.OnInputUp += InputManager_OnInputUp;
	}

	CommonNode selectedNode;
	Vector2 originalPos;
	CommonNodeData data;

	void InputManager_OnInputDown (CommonNode node, bool ignored, Vector2 pos)
	{
		selectedNode = null;
		if (node == null){return;}
		selectedNode = node;
	}

	void InputManager_OnInputUp (CommonNode node, bool hasMoved, Vector2 pos)
	{
		data = null;
		if (node == null){return;}
		if (selectedNode != node){return;}
		if (hasMoved){return;}

		if (GameInstance.NodeCanvas.SelectedNode != node)
		{
			if (GameInstance.NodeCanvas.SelectedNode != null)
				GameInstance.NodeCanvas.SelectedNode.SetSelected(false);
			GameInstance.NodeCanvas.SelectedNode = node;
			GameInstance.NodeCanvas.SelectedNode.SetSelected(true);
		}

		//if (GameInstance.NodeCanvas.SelectedNode != node)
		//{
		//	GameInstance.NodeCanvas.SelectedNode = node;
		//	node.InputField.enabled = true;
		//	return;
		//}

		//when input field is confirmed, disable

		//if (GameInstance.NodeCanvas.SelectedNode != node)
		//{
		//	GameInstance.NodeCanvas.SelectedNode = node;
		//	node.InputField.enabled = true;
		//	return;
		//}

		//NodeCanvas.

		//data = selectedNode.data;
		//GameInstance.NodeCanvas.keyboard = TouchScreenKeyboard.Open(data.Name,TouchScreenKeyboardType.Default,true,false);

		//selectedNode.data.Position = pos;
		//selectedNode.transform.position = selectedNode.data.Position;
		//NodeCanvas.MovedNodeEvent(selectedNode);
		//selectedNode = null;
		//node.InputField.ActivateInputField();

		//selectedNode.InputField.enabled = true;

	}

	//void OnGUI()
	//{
	//	if (GameInstance.NodeCanvas.keyboard != null && GameInstance.NodeCanvas.keyboard.status == TouchScreenKeyboard.Status.Visible)
	//	{
	//		data.Name = GameInstance.NodeCanvas.keyboard.text;
	//		data.SetDirty();
	//	}
	//}

	public override void DisableTool ()
	{
		base.DisableTool ();
		InputManager.OnInputDown -= InputManager_OnInputDown;
		InputManager.OnInputUp -= InputManager_OnInputUp;
	}
}
