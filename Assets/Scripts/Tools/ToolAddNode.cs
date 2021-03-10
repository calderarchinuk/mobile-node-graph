using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolAddNode : ToolBase
{
	public override void EnableTool ()
	{
		base.EnableTool ();
		InputManager.OnInputUp += InputManager_OnInputUp;
	}
	public Text nodeNameInput;

	CommonNodeData data;
	CommonNode sn;

	void InputManager_OnInputUp (CommonNode node, bool hasMoved, Vector2 pos)
	{
		if (node != null){return;}
		if (hasMoved){return;}

		if (GameInstance.NodeCanvas.SelectedNode != null)
		{
			GameInstance.NodeCanvas.SelectedNode.SetSelected(false);
			GameInstance.NodeCanvas.SelectedNode = null;
		}
		else
		{
			data = new CommonNodeData();
			Debug.Log("create node");
			data.Position = pos;
			data.id = Random.Range(0,99999).ToString();
			data.type = "basic";

			var newNode = GameInstance.NodeCanvas.AddNode(data);
			GameInstance.NodeCanvas.SelectedNode = newNode;
			newNode.SetSelected(true);
		}
		//node.InputField.ActivateInputField();
		//node.InputField.

		//GameInstance.NodeCanvas.keyboard = TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default,true,false);
		//BUG creating nodes without settings a value breaks things
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
		InputManager.OnInputUp -= InputManager_OnInputUp;
	}
}
