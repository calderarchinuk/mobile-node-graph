using UnityEngine;
using System.Collections;

public class GradientBackground : MonoBehaviour {
	
	public Color topColor = Color.blue;
	public Color bottomColor = Color.white;

	float speed = 0.03f;

	int gradientLayer = 7;

	Mesh customMesh;

	void Awake ()
	{
		gradientLayer = Mathf.Clamp(gradientLayer, 0, 31);

		foreach (Camera cam in Object.FindObjectsOfType(typeof(Camera)))
		{
			cam.clearFlags = CameraClearFlags.Depth;
			cam.cullingMask = cam.cullingMask & ~(1 << gradientLayer);
		}

		Camera gradientCam = new GameObject("Gradient Cam",typeof(Camera)).GetComponent<Camera>();
		//DontDestroyOnLoad(gradientCam);
		gradientCam.depth = -100;
		gradientCam.cullingMask = 1 << gradientLayer;
		
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[4]
		{new Vector3(-100f, .577f, 1f), new Vector3(100f, .577f, 1f), new Vector3(-100f, -.577f, 1f), new Vector3(100f, -.577f, 1f)};
		
		mesh.colors = new Color[4] {topColor,topColor,bottomColor,bottomColor};
		
		mesh.triangles = new int[6] {0, 1, 2, 1, 3, 2};
		
		Material mat = Resources.Load<Material>("GradientMaterial");
		GameObject gradientPlane = new GameObject("Gradient Plane", typeof(MeshFilter), typeof(MeshRenderer));
		//DontDestroyOnLoad(gradientPlane);

		((MeshFilter)gradientPlane.GetComponent(typeof(MeshFilter))).mesh = mesh;
		customMesh = gradientPlane.GetComponent<MeshFilter>().mesh;
		gradientPlane.GetComponent<Renderer>().material = mat;
		gradientPlane.layer = gradientLayer;
	}

	void Update()
	{
		customMesh.colors = new Color[4] {
			Color.Lerp(customMesh.colors[0],topColor,speed),
			Color.Lerp(customMesh.colors[1],topColor,speed),
			Color.Lerp(customMesh.colors[2],bottomColor,speed),
			Color.Lerp(customMesh.colors[3],bottomColor,speed)};
	}
}