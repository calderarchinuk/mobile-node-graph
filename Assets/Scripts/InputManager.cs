using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//only used for input events - gamestates determine what happens

public class InputManager : MonoBehaviour
{
	//Camera nodeCamera;
	//float dragSpeed = -0.05f;
	
	//float orthoZoomSpeed = 0.1f;        // The rate of change of the orthographic size in orthographic mode.
	//bool CreateOnRelease = false;

	public delegate void onInputEvent(CommonNode node, bool hasMoved, Vector2 pos);

	///after there is an input pressed. node is null if pressed over nothing. hasmoved always false
	public static event onInputEvent OnInputDown;
	public static void InputDownEvent(CommonNode node, bool hasMoved, Vector2 pos) { if (OnInputDown != null) { OnInputDown(node, hasMoved, pos); } }

	///after there is an input released. node is null if released over nothing. hasmoved = false if this input is a 'tap'
	public static event onInputEvent OnInputUp;
	public static void InputUpEvent(CommonNode node, bool hasMoved, Vector2 pos) { if (OnInputUp != null) { OnInputUp(node, hasMoved, pos); } }

	public delegate void onTrashNode();
	public static event onTrashNode OnTrashNode;
	public static void TrashNodeEvent(){if (OnTrashNode != null){OnTrashNode();}}

	void Start ()
	{
		//nodeCamera = Camera.main;
	}

	public static void DrawCircle(Vector3 position, Vector3 normal, float radius, Color color, float time = 0)
	{
		Vector3 left = Vector3.Cross(normal, Vector3.up).normalized;

		var r = left * radius;
		var q = Quaternion.AngleAxis(360f / 32, normal);
		for (int i = 0; i < 32; i++) {
			var tempR = q * r;
			Debug.DrawLine(position+r,position+tempR,color,time);
			r = tempR;
		}
	}

	private static List<RaycastResult> tempRaycastResults = new List<RaycastResult>();

	public bool PointIsOverUI(float x, float y)
	{
		var eventDataCurrentPosition = new PointerEventData(EventSystem.current);

		eventDataCurrentPosition.position = new Vector2(x, y);

		tempRaycastResults.Clear();

		EventSystem.current.RaycastAll(eventDataCurrentPosition, tempRaycastResults);

		int nonNodeHits = 0;
		foreach(var v in tempRaycastResults)
		{
			if (v.gameObject.GetComponentInParent<CommonNode>() == null)
			{
				nonNodeHits ++;
			}
		}
		return nonNodeHits > 0;
	}

	public bool PointIsOverTrash(float x, float y)
	{
		var eventDataCurrentPosition = new PointerEventData(EventSystem.current);

		eventDataCurrentPosition.position = new Vector2(x, y);

		tempRaycastResults.Clear();

		EventSystem.current.RaycastAll(eventDataCurrentPosition, tempRaycastResults);

		foreach(var v in tempRaycastResults)
		{
			if (v.gameObject.GetComponent<Trash>() != null)
			{
				return true;
			}
		}
		return false;
	}

	Vector2 inputStartPos;
	float tapDistance = 50;
	float FingerRadius = 80;

	void OnDestroy()
	{
		OnInputDown = null;
		OnInputUp = null;
		OnTrashNode = null;
	}

	void Update ()
	{
		//if hits selection any UI, ignore

		if (Input.touchCount == 1)
		{
			var t = Input.GetTouch(0);

			//begin single touch
			RaycastHit hit = new RaycastHit();
			DrawCircle(t.position,Vector3.forward,80,Color.white);
			if (t.phase == TouchPhase.Began)
			{
				bool hitSomething = false;
				//cameraDragMagnitude = 0;
				if (PointIsOverUI(t.position.x,t.position.y))
				{
					Debug.Log("begin point over ui");
					//disable a bunch of stuff
					return;
				}
				var pos3 = (Vector3)t.position + Vector3.back * 500;

				inputStartPos = t.position;
				if (Physics.SphereCast(pos3,FingerRadius,Vector3.forward,out hit, 500))
				{
					var sn = hit.collider.GetComponent<CommonNode>();
					if (sn != null)
					{
						InputDownEvent(sn,false,t.position);
						hitSomething = true;
						DrawCircle(t.position,Vector3.forward,100,Color.red,1);
					}
				}

				if (!hitSomething)
				{
					InputDownEvent(null,false,t.position);
					DrawCircle(t.position,Vector3.forward,100,Color.green,1);
				}
			}

			//drag drop on node
			if (t.phase == TouchPhase.Ended)
			{
				bool hitSomething = false;
				if (PointIsOverUI(t.position.x,t.position.y))
				{
					if (PointIsOverTrash(t.position.x,t.position.y))
					{
						TrashNodeEvent();
					}
					else
					{
						Debug.Log("end point over ui");
					}
					//check if there's a selected node
					//check if over trash icon

					//disable a bunch of stuff
					return;
				}

				var pos3 = (Vector3)t.position + Vector3.back * 500;

				bool hasMoved = Vector2.Distance(t.position,inputStartPos) > tapDistance;

				Debug.DrawRay(pos3,Vector3.forward * 200,Color.red,10);
				if (Physics.SphereCast(pos3,FingerRadius,Vector3.forward,out hit, 500))
				{
					var sn = hit.collider.GetComponent<CommonNode>();
					if (sn != null)
					{
						InputUpEvent(sn,hasMoved,t.position);
						DrawCircle(t.position,Vector3.forward,100,Color.red,1);
						hitSomething = true;
					}
				}

				if (!hitSomething)
				{
					InputUpEvent(null,hasMoved,t.position);
					DrawCircle(t.position,Vector3.forward,100,Color.green,1);

					//todo if not created node
					//if (GameInstance.NodeCanvas.SelectedNode != null)
					//{
					//	GameInstance.NodeCanvas.SelectedNode.SetSelected(false);
					//}
					//GameInstance.NodeCanvas.SelectedNode = null;
				}
			}

			/*
			//drag camera
			if (DragStartNode == null && !PointIsOverUI(t.position.x,t.position.y))
			{
				nodeCamera.transform.position = new Vector3(
					nodeCamera.transform.position.x + Input.GetTouch(0).deltaPosition.x * dragSpeed,
					nodeCamera.transform.position.y + Input.GetTouch(0).deltaPosition.y * dragSpeed,
					nodeCamera.transform.position.z);

				cameraDragMagnitude += Input.GetTouch(0).deltaPosition.x;
				cameraDragMagnitude += Input.GetTouch(0).deltaPosition.y;
			}*/
		}

		//TODO drag start and end callbacks for the editor mode fall through to move camera

		return;
		/*if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			DrawCircle(touchOne.position,Vector3.forward,100,Color.cyan);
			DrawCircle(touchZero.position,Vector3.forward,100,Color.cyan);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// ... change the orthographic size based on the change in distance between the touches.
			nodeCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
			
			// Make sure the orthographic size never drops below zero.
			nodeCamera.orthographicSize = Mathf.Clamp(nodeCamera.orthographicSize,4,20);

			DragStartNode = null;
			CreateOnRelease = false;
		}

		if (Input.touchCount == 0)
		{
			switch (GameInstance.Instance.GameState) {
			case GameState.Menu:
				if (MainMenuCanvas.SelectedMenuNode != null)
				nodeCamera.transform.position = Vector3.Lerp(nodeCamera.transform.position,MainMenuCanvas.SelectedMenuNode.transform.position+Vector3.back*10,0.01f);
				break;
			case GameState.Editor:
				if (EditorModeCanvas.SelectedNode != null)
				nodeCamera.transform.position = Vector3.Lerp(nodeCamera.transform.position,EditorModeCanvas.SelectedNode.transform.position+Vector3.back*10,0.01f);
				break;
			case GameState.Story:
				if (StoryCanvas.SelectedNode != null)
				nodeCamera.transform.position = Vector3.Lerp(nodeCamera.transform.position,StoryCanvas.SelectedNode.transform.position+Vector3.back*10,0.01f);
				break;
			default:
				throw new System.ArgumentOutOfRangeException ();
			}
		}*/
	}
}
