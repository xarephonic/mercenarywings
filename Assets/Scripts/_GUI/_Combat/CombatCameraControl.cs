using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CombatCameraControl : MonoBehaviour {

	public Transform lookAtTarget;

	public float defaultZoomLevel;
	public float zoomMin;
	public float zoomMax;

	public float xMin;
	public float xMax;

	public float defaultX;
	public float defaultY;

	float zoomLevel;

	float xRot;
	float yRot;

	private float touchDistance;

	private GameObject fakeObject;
	private float lerpSpeed;

	// Use this for initialization
	void Awake () {
		zoomLevel = defaultZoomLevel;

		xRot = defaultX;
		yRot = defaultY;
	}

	void Start()
	{
		lerpSpeed = 0.3f;

		fakeObject = new GameObject();
		fakeObject.transform.position = transform.position;
		fakeObject.name = "CameraTargetPosition";
	}
	
	// Update is called once per frame
	void Update () {
	
		lookAtTarget = CombatSelectionHandler.selectedObject.transform;

		bool touchingInsideCameraMoveArea = true;

		if(Input.touchCount == 0)
		{
			touchingInsideCameraMoveArea = false;
		}

		foreach(Touch t in Input.touches)
		{
			if(t.phase != TouchPhase.Ended || t.phase != TouchPhase.Canceled)
			{
				if(EventSystem.current.IsPointerOverGameObject(t.fingerId))
				{
					touchingInsideCameraMoveArea = false;
					break;
				}
			}
		}

		if(!touchingInsideCameraMoveArea)
		{
			lerpSpeed = 0.3f;
		}
		else
		{
			lerpSpeed = 1.0f;
		}

		if(Input.touchCount == 2)
		{
			if(touchDistance == 0)
			{
				touchDistance = Vector2.Distance(Input.touches[0].position,Input.touches[1].position);
			}
			else
			{
				float distance = Vector2.Distance(Input.touches[0].position,Input.touches[1].position);

				float diff = distance - touchDistance;

				zoomLevel -= diff*Time.deltaTime;

				touchDistance = distance;
			}
		}
		else if(Input.touchCount > 0)
		{
			for (int i = 0; i < Input.touchCount; i++) {
				if(Input.touches[i].phase == TouchPhase.Moved)
				{	
					yRot += Input.touches[i].deltaPosition.x;
					xRot -= Input.touches[i].deltaPosition.y/2;

					xRot = Mathf.Clamp(xRot,xMin,xMax);
				}
			}
		}
		else
		{
			touchDistance = 0;
		}

		zoomLevel = Mathf.Clamp(zoomLevel,zoomMin,zoomMax);
		
		fakeObject.transform.position = lookAtTarget.transform.position;

		fakeObject.transform.eulerAngles = new Vector3(xRot,yRot,0);

		fakeObject.transform.position -= fakeObject.transform.forward*zoomLevel;

		transform.position = Vector3.Lerp(transform.position,fakeObject.transform.position,lerpSpeed);

		transform.eulerAngles = new Vector3(xRot,yRot,0);



	}
}
