using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializer
{
	byte[] Serialize(GraphContainer graphContainer);
	GraphContainer Deserialize(byte[] data);
}
