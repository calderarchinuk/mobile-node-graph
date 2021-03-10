using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFileButton : MonoBehaviour
{
	public string path;
	public Text filenameText;

	public void Start()
	{
		string filename = System.IO.Path.GetFileName(path);
		filenameText.text = filename;
	}

	public void Button_Load()
	{
		Debug.Log("load " + path);


		var text = System.IO.File.ReadAllText(path);
		var settings = new Newtonsoft.Json.JsonSerializerSettings();
		settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
		var gc = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphContainer>(text,settings);
		GameInstance.NodeCanvas.LoadGraphContainer(gc);

		//gameinstance clear canvas
		//gameinstance load canvas from this data
	}

	public void Button_Delete()
	{
		System.IO.File.Delete(path);
		FindObjectOfType<MenuCanvas>().RefreshLoadedFiles();
	}
}
