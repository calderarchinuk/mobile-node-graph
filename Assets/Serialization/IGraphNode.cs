using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializable<T>
{
	string id{get;set;}
	string type{get;set;}

	string Serialize();
	void Deserialize(T type);
}

//this will hold temporary data needed to create gameobjects or other representations
public interface IGraphNode : ISerializable<IGraphNode>
{
	int x{get;set;}
	int y{get;set;}
}

//this will hold temporary data needed to create gameobjects or other representations
public interface IEdge : ISerializable<IEdge>
{
	string source{get;set;}
	string target{get;set;}
}


public class Graph
{
	public string name = "graph name";

	//metadata
	public Dictionary<string,IGraphNode> nodes = new Dictionary<string,IGraphNode>();
	public List<IEdge> edges = new List<IEdge>();


	public string Serialize ()
	{
		return "";
	}

	public void Deserialize (Graph graphdata)
	{
		
	}
}

public class GraphContainer
{
	public string name;

	//metadata
	public List<Graph> graphs = new List<Graph>();

	public GraphContainer()
	{

	}

	public string Serialize ()
	{
		var settings = new Newtonsoft.Json.JsonSerializerSettings();
		settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
		return Newtonsoft.Json.JsonConvert.SerializeObject(this,settings);
	}

	public void Deserialize (GraphContainer graphdata)
	{
		//call constructors everywhere
	}
}