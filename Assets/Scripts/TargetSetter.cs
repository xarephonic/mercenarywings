using UnityEngine;
using System.Collections;

public class TargetSetter : MonoBehaviour {

	public static void SetTargetForSelectedPlane(GameObject target){
		PlayerPlaneSelectionHandler.selectedPlane.GetComponent<TrackingModule>().SetTarget(target);
		PlayerPlaneSelectionHandler.selectedPlane.GetComponent<AircraftFireControl>().SetTarget(target);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
