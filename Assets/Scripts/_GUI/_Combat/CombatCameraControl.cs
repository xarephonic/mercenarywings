using UnityEngine;
using System.Collections;

public class CombatCameraControl : MonoBehaviour {

	public Transform lookAtTarget;

	public float zoomLevel = 10;

	public float xRot;
	public float yRot;

	private float touchDistance;

	public LineRenderer line;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		lookAtTarget = CombatSelectionHandler.selectedObject.transform;

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

				zoomLevel += diff*Time.deltaTime;

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

					xRot = Mathf.Clamp(xRot,-90,90);
				}
			}
		}
		else
		{
			touchDistance = 0;
		}

		zoomLevel = Mathf.Clamp(zoomLevel,10,50);
		
		transform.position = lookAtTarget.transform.position;

		transform.eulerAngles = new Vector3(xRot,yRot,0);

		transform.position -= transform.forward*zoomLevel;



	}
}
