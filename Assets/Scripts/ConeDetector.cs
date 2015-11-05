using UnityEngine;
using System.Collections;

public class ConeDetector : MonoBehaviour {

	public PlaneMoveControl myPlane;

	void OnTriggerEnter(Collider other)
	{
		if(myPlane != null && PlaneSelector.currentMode == PlaneSelector.PlayMode.playMode)
		{
			if(other.tag == "Enemy")
			{
				myPlane.Fire();
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(myPlane != null && PlaneSelector.currentMode == PlaneSelector.PlayMode.playMode)
		{
			if(other.tag == "Enemy")
			{
				myPlane.StopFire();
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
