using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for togglable tools
//these listen for specific input events and do stuff

public class ToolBase : MonoBehaviour
{
	public bool EnabledByDefault;

	public virtual void Start()
	{
		if (EnabledByDefault){EnableTool();}
	}
	public virtual void EnableTool()
	{
		
	}

	public virtual void DisableTool()
	{

	}


}
