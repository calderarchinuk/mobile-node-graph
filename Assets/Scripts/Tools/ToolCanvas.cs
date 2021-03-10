using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//intended to have common buttons and settings display while editing nodes

public class ToolCanvas : MonoBehaviour
{
	public RectTransform MainPanel;

	[ContextMenu("hide")]
	public void Button_Hide()
	{
		MainPanel.anchoredPosition = new Vector2(-Screen.width,0);
	}
	[ContextMenu("show")]
	public void Button_Show()
	{
		MainPanel.anchoredPosition = new Vector2(0,0);
	}
}
