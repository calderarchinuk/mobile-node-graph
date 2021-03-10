using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineHelper : MonoBehaviour
{
	public ConnectionData data;

	public CommonNode source;
	public CommonNode dest;

	//called on creation and deserialization
	public void Initialize (CommonNode startNode, CommonNode endNode)
	{
		data = new ConnectionData(Random.Range(0,999999).ToString(),startNode.data.id,endNode.data.id);
		source = startNode;
		dest = endNode;
		NodeCanvas.OnNodeMoved += EditorModeCanvas_OnNodeMoved;
		NodeCanvas.OnDeleteNode += EditorModeCanvas_OnDeleteNode;
		EditorModeCanvas_OnNodeMoved(source);
	}

	public void Initialize (ConnectionData cd)
	{
		data = cd;

		var nodes = FindObjectsOfType<CommonNode>();
		foreach(var n in nodes)
		{
			if (n.data.id == cd.source)
				source = n;
			if (n.data.id == cd.target)
				dest = n;
		}
		NodeCanvas.OnNodeMoved += EditorModeCanvas_OnNodeMoved;
		NodeCanvas.OnDeleteNode += EditorModeCanvas_OnDeleteNode;
		EditorModeCanvas_OnNodeMoved(source);
	}

	public void SetTargets (CommonNode source, CommonNode destination)
	{
		this.source = source;
		dest = destination;
		EditorModeCanvas_OnNodeMoved(source);
	}

	void EditorModeCanvas_OnDeleteNode (CommonNode newSelectedNode)
	{
		if (newSelectedNode == source || newSelectedNode == dest)
		{
			Destroy(gameObject);
		}
	}

	void EditorModeCanvas_OnNodeMoved (CommonNode node)
	{
		if (node == source || node == dest)
		{
			var uiLine = GetComponent<UILineRenderer>();
			uiLine.Points[0] = source.transform.position;
			uiLine.Points[1] = dest.transform.position;
			uiLine.SetVerticesDirty();
		}
	}

	void OnDestroy()
	{
		NodeCanvas.OnNodeMoved -= EditorModeCanvas_OnNodeMoved;
		NodeCanvas.OnDeleteNode -= EditorModeCanvas_OnDeleteNode;
	}
}
