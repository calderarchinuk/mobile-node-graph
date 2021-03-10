using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//displays new/save/load buttons

public class MenuCanvas : MonoBehaviour
{
	public RectTransform MainPanel;
	public GameObject ButtonPrefab;
	public Transform ButtonRoot;
	List<LoadFileButton> LoadFileButtons = new List<LoadFileButton>();

	void Start ()
	{
		//start hidden
		HideImmediate();
	}

	public void RefreshLoadedFiles()
	{
		for(int i = LoadFileButtons.Count-1;i>=0;i--)
			Destroy(LoadFileButtons[i].gameObject);
		LoadFileButtons.Clear();

		//destroy and reload all buttons for each file
		foreach(var v in GameInstance.Instance.GetFiles())
		{
			var go = Instantiate(ButtonPrefab,Vector3.zero,Quaternion.identity);
			go.transform.SetParent(ButtonRoot);
			var lf = go.GetComponent<LoadFileButton>();
			lf.path = v;
			LoadFileButtons.Add(lf);
		}
	}

	[ContextMenu("hide")]
	void HideImmediate()
	{
		MainPanel.anchoredPosition = new Vector2(-Screen.width,0);
	}

	public void Button_Hide()
	{
		//improvement coroutine
		MainPanel.anchoredPosition = new Vector2(-Screen.width,0);
	}

	[ContextMenu("show")]
	public void Button_Show()
	{
		MainPanel.anchoredPosition = new Vector2(0,0);
		RefreshLoadedFiles();
	}

	public void Button_New()
	{
		//reload scene?
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void Button_Save()
	{
		//write story data to file

		//ISerializer serializer = new JsonGraphSerializer();
		GraphContainer gc = new GraphContainer();
		gc.graphs.Add(new Graph());

		var nodes = GameObject.FindObjectsOfType<CommonNode>();
		foreach(var n in nodes)
		{
			gc.graphs[0].nodes.Add(n.data.id.ToString(),n.data);
		}

		var lines = GameObject.FindObjectsOfType<LineHelper>();
		foreach(var l in lines)
		{
			gc.graphs[0].edges.Add(l.data);
		}
		gc.name = GameInstance.Instance.graphName;

		GameInstance.WriteToDisk(gc);
		RefreshLoadedFiles();

	}
}
