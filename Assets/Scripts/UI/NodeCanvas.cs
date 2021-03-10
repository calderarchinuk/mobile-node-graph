using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//game instance finds and writes to this
//displays stuff


public class NodeCanvas : MonoBehaviour
{
	public GameObject StoryNodePrefab;
	public GameObject LineHelper;
	public CommonNode SelectedNode;

	public Transform Root;
	public TouchScreenKeyboard keyboard;

	public delegate void onNodeChanged(CommonNode newSelectedNode);

	///after the game state has changed
	public static event onNodeChanged OnSelectedNodeChanged;
	public static void SelectedNodeChangedEvent(CommonNode newSelectedNode) { if (OnSelectedNodeChanged != null) { OnSelectedNodeChanged(newSelectedNode); } }

	///after the node has moved. TODO redraw all connection lines
	public static event onNodeChanged OnNodeMoved;
	public static void MovedNodeEvent(CommonNode node){if (OnNodeMoved != null) { OnNodeMoved(node); }}

	///after the node has been deleted
	public static event onNodeChanged OnDeleteNode;
	public static void DeleteNodeEvent(CommonNode node) { if (OnDeleteNode != null) { OnDeleteNode(node); } }

	public void LoadGraphContainer(GraphContainer data)
	{
		//clear everything
		ClearAll();

		//if there's any nodes and/or edges, set that
		foreach(var g in data.graphs)
		{
			foreach(var kvp in g.nodes)
			{
				SetNode(kvp.Key,kvp.Value);
				//AddNode(n as StoryNodeData);
			}

			foreach(var e in g.edges)
			{
				SetConnection(e as ConnectionData);
			}
		}

		//set graph container name (in game instance)
		GameInstance.Instance.SetGraphName(data.name);

		FindObjectOfType<MenuCanvas>().Button_Hide();
		FindObjectOfType<ToolCanvas>().Button_Show();
	}

	public CommonNode AddNode(CommonNodeData data)
	{
		var node = Instantiate(StoryNodePrefab,data.Position,Quaternion.identity,transform);
		var sn = node.GetComponent<CommonNode>();
		sn.transform.parent = Root;
		sn.data = data;
		//CurrentData.NodeData.Add(sn.data);
		sn.Initialize();

		SelectedNode = sn;
		SelectedNodeChangedEvent(sn);
		return sn;
		//Dict.Add(sn,sn.data);
	}

	public void SetNode(string id, IGraphNode data)
	{
		Vector2 pos = new Vector2(data.x,data.y);
		var node = Instantiate(StoryNodePrefab,pos,Quaternion.identity,transform);
		var sn = node.GetComponent<CommonNode>();
		sn.transform.parent = Root;

		//var snd = data as StoryNodeData;

		data.id = id;
		sn.data = data as CommonNodeData; //need to cast using type

		//CurrentData.NodeData.Add(sn.data);
		sn.Initialize();
		sn.SetSelected(false);

		//SelectedNode = sn;
		//SelectedNodeChangedEvent(sn);
	}

	public void ToggleConnection (CommonNode startNode, CommonNode endNode)
	{
		var lineHelper = IsConnected(startNode,endNode);

		if (lineHelper != null)
		{
			RemoveConnection(lineHelper);
		}
		else
		{
			AddConnection(startNode,endNode);
		}

		//CurrentData.ConnectionData.Add(
	}

	LineHelper IsConnected(CommonNode startNode, CommonNode endNode)
	{
		var helpers = FindObjectsOfType<LineHelper>();
		foreach(var line in helpers)
		{
			if (line.dest == startNode && line.source == endNode)
			{
				return line;
			}
			if (line.dest == endNode && line.source == startNode)
			{
				return line;
			}
		}
		return null;
	}

	public void AddConnection (CommonNode startNode, CommonNode endNode)
	{
		//var conn = new ConnectionData(Random.Range(0,99999),startNode.data.Id,endNode.data.Id);

		var node = Instantiate(LineHelper,Vector3.zero,Quaternion.identity,transform);
		var lh = node.GetComponent<LineHelper>();
		lh.Initialize(startNode,endNode);
		lh.transform.SetAsFirstSibling();
	}

	void SetConnection(ConnectionData cd)
	{
		var node = Instantiate(LineHelper,Vector3.zero,Quaternion.identity,transform);
		var lh = node.GetComponent<LineHelper>();
		lh.Initialize(cd);
		lh.transform.SetAsFirstSibling();
	}

	public void RemoveConnection (LineHelper line)
	{
		Destroy(line.gameObject);
	}

	public void CreateLineRendererConnection(CommonNode source, CommonNode destination)
	{
		GameObject golr = new GameObject("Line");
		LineRenderer lr = golr.AddComponent<LineRenderer>();
		var helper = golr.AddComponent<LineHelper>();
		helper.SetTargets(source,destination);
		lr.SetWidth(0.2f,0.2f);
		lr.material = Resources.Load<Material>("NormalLineMat");
		golr.transform.SetParent(source.transform);
	}

	public void DestroyLineRenderers(CommonNode source, CommonNode destination)
	{
		foreach(var helper in source.GetComponentsInChildren<LineHelper>())
		{
			if (helper.dest == destination)
			{
				Destroy(helper.gameObject);
			}
		}

		foreach(var helper in destination.GetComponentsInChildren<LineHelper>())
		{
			if (helper.dest == source)
			{
				Destroy(helper.gameObject);
			}
		}
	}

	void FindDestroyAllConnectedLines (CommonNode selectedNode)
	{
		var helpers = FindObjectsOfType<LineHelper>();
		foreach(var h in helpers)
		{
			if (h.dest == selectedNode || h.source == selectedNode)
			{
				Destroy(h.gameObject);
			}
		}
	}

	public void ClearAll()
	{
		//also mess up the data so the stuff spawned later this frame don't connect to the wrong nodes
		//UGH UNITY

		var nodes = FindObjectsOfType<CommonNode>();
		foreach(var n in nodes)
		{
			n.data.id = "";
			Destroy(n.gameObject);
		}

		var helpers = FindObjectsOfType<LineHelper>();
		foreach(var h in helpers)
		{
			h.dest = null;
			h.source = null;
			Destroy(h.gameObject);
		}
	}
}
