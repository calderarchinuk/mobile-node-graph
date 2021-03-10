using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionDirection
{
	TwoWay,
	None,
	StartToEnd,
	EndToStart
}

//used for serialization

public class ConnectionData : IEdge
{
	//public int Id;
	//public int StartId;
	//public int EndId;

	public string UnlockKey; //sets a string when activated
	public string Requirement; //string must be set for this to be visible

	public ConnectionDirection direction = ConnectionDirection.TwoWay;

	public ConnectionData(string _id, string startid, string endid)
	{
		id = _id;
		source = startid;;
		target = endid;
		type = "basic";
	}

	#region ISerializable implementation

	public string Serialize ()
	{
		return "";
	}

	public void Deserialize (IEdge type)
	{
		
	}

	public string id {get;set;}

	public string type {
		get;set;
	}

	#endregion

	#region IEdge implementation

	public string source {
		get;set;
	}

	public string target {
		get;set;
	}

	#endregion
}
