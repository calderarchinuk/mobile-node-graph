using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds all the data for each node in a story and how to connect with others

public class CommonNodeData : IGraphNode
{
	//public int Id;
	[Newtonsoft.Json.JsonIgnore]
	public Vector2 Position
	{
		get
		{
			return new Vector2(x,y);
		}
		set
		{
			x = (int)value.x;
			y = (int)value.y;
		}
	}
	public string Name;
	public string Description;

	public string UnlockKey; //sets a string when activated
	public string Requirement; //string must be set for this to be visible

	#region ISerializable implementation

	public string Serialize ()
	{
		return "";
	}

	public void Deserialize (IGraphNode type)
	{
		
	}

	public string id
	{
		get;set;
	}

	public string type {
		get;set;
	}

	#endregion

	#region IGraphNode implementation

	public int x {
		get;set;
	}

	public int y {
		get;set;
	}

	#endregion
}
