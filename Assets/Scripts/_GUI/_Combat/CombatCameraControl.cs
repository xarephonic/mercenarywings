using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CombatCameraControl : MonoBehaviour {

	public static CombatCameraControl instance;

	public Transform lookAtTarget;

	public void SetTarget(Transform newTarget){
		lookAtTarget = newTarget;
	}

	public bool rotateMode;

	public float defaultXRot;
	public float defaultYRot;
	public float xRotMin = -90.0f;
	public float xRotMax = 90.0f;
	public float rotateSpeed;

	public Vector2 targetRotation;

	public float defaultZoom;
	public float zoomMin = 40.0f;
	public float zoomMax = 1800.0f;
	public float zoomSpeed;

	public float targetZoom;

	public float moveSpeed;

	private GameObject cube;

	public void HandleTouchCameraControl(){

		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			if(EventSystem.current.IsPointerOverGameObject(touchZero.fingerId) || EventSystem.current.IsPointerOverGameObject(touchOne.fingerId)){
				return;
			}

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			targetZoom += deltaMagnitudeDiff;

			targetZoom = Mathf.Clamp(targetZoom,zoomMin,zoomMax);
		}
		else if(Input.touchCount == 1)
		{
			if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
				return;
			}

			if(rotateMode){
				Vector2 movement = Input.touches[0].deltaPosition;

				targetRotation += new Vector2(movement.y,movement.x);

				targetRotation = new Vector2(Mathf.Clamp(targetRotation.x,xRotMin,xRotMax),targetRotation.y);
			}
		}
	}

	// Use this for initialization
	void Awake () {
		targetZoom = defaultZoom;

		targetRotation = new Vector2(defaultXRot,defaultYRot);
	}

	void Start()
	{
		
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "TestyTheCube";
		cube.GetComponent<MeshRenderer>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		HandleTouchCameraControl();
	
		lookAtTarget = PlayerPlaneSelectionHandler.selectedPlane.transform;

		Vector3 targetCameraPosition = Vector3.zero;

		targetCameraPosition = Vector3.forward*targetZoom;

		targetCameraPosition = Quaternion.Euler(targetRotation.x, targetRotation.y,0) * targetCameraPosition;

		targetCameraPosition += lookAtTarget.position;

		cube.transform.position = targetCameraPosition;


		if(Vector3.Distance(Camera.main.transform.position,targetCameraPosition) > 1)
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,targetCameraPosition,moveSpeed);

		Camera.main.transform.eulerAngles = new Vector3(targetRotation.x*-1 , targetRotation.y+180 ,0);

		cube.transform.eulerAngles = Camera.main.transform.eulerAngles;

		//Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,targetZoom,zoomSpeed);
	}
}
