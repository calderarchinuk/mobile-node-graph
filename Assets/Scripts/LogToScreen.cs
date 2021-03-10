using UnityEngine;
using System.Collections;

public class LogToScreen : MonoBehaviour
{
	string myLog;
	Queue myLogQueue = new Queue();

	void OnEnable ()
	{
		ShakeInputEvent.OnShakeInput += ShakeInputEvent_OnShakeInput;
	}

	void ShakeInputEvent_OnShakeInput ()
	{
		if (Input.touchCount > 0)
		{
			ShakeInputEvent.OnShakeInput -= ShakeInputEvent_OnShakeInput;
			Application.logMessageReceived += HandleLog;
		}
	}

	void OnDisable ()
	{
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string logString, string stackTrace, LogType type){
		myLog = logString;
		string newString = "\n [" + type + "] : " + myLog;
		myLogQueue.Enqueue(newString);
		if (type == LogType.Exception)
		{
			newString = "\n" + stackTrace;
			myLogQueue.Enqueue(newString);
		}
		myLog = string.Empty;
		foreach(string mylog in myLogQueue){
			myLog += mylog;
		}
	}

	void OnGUI () {
		GUILayout.Label(myLog);
	}
}