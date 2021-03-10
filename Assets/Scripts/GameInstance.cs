using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum GameState
{
	Menu,
	Editor,
	Story,
	Notepad
}

//deals with basic app stuff. file loading/saving

public class GameInstance : MonoBehaviour
{
	public static GameInstance Instance;

	static NodeCanvas nodeCanvas;
	public static NodeCanvas NodeCanvas
	{
		get
		{
			if (nodeCanvas == null)
			{
				nodeCanvas = FindObjectOfType<NodeCanvas>();
			}
			return nodeCanvas;
		}
	}

	public GameState GameState = GameState.Menu;

	public delegate void onGameStateChanged(GameState state);
	///after the game state has changed
	public static event onGameStateChanged OnGameStateChanged;
	public static void GameStateChangedEvent(GameState state) { if (OnGameStateChanged != null) { OnGameStateChanged(state); } }

	public string[] LoadedFiles;

	public string Properties;

	static string DOWNLOADDIR = "/sdcard/download/StoryNodes/";

	public string[] GetFiles()
	{
		#if UNITY_EDITOR
		var debugPath = Application.streamingAssetsPath+"/editor_test.txt";
		LoadedFiles = new string[1]{debugPath};
		Debug.Log(debugPath);
		return LoadedFiles;
		#else

		if (Directory.Exists(DOWNLOADDIR))
		{
			LoadedFiles = System.IO.Directory.GetFiles(DOWNLOADDIR);
			return LoadedFiles;
		}
		else
		{
			return new string[0];
		}
		#endif
	}

	void OnEnable()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		if (Application.isEditor)
		{
			DOWNLOADDIR = Application.streamingAssetsPath+"/";
		}

		GetFiles();
	}

	public static void WriteToDisk(GraphContainer graphContainer)
	{
		//IMPROVEMENT choose a serializer or something
		//var serialized = JsonUtility.ToJson(data);

		if (string.IsNullOrEmpty(graphContainer.name))
		{
			Debug.LogError("graph doesn't have a name!");
			return;
		}

		Directory.CreateDirectory(DOWNLOADDIR);

		var contents = graphContainer.Serialize();
		Debug.Log(contents);

		string _path = DOWNLOADDIR+graphContainer.name+".txt";
		Debug.Log(graphContainer.name + " wrote to path "+ _path);
		System.IO.File.WriteAllText(_path,contents);
	}

	void SetGameState(GameState newGameState)
	{
		GameState = newGameState;
		GameStateChangedEvent(newGameState);
	}

	//TODO move this elsewhere
	public string graphName;
	public void SetGraphName(string name)
	{
		graphName = name;
	}
}
