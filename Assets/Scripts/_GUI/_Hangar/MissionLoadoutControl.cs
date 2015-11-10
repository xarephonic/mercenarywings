using UnityEngine;
using System.Collections;

public class MissionLoadoutControl : MonoBehaviour {

	public MissionLoader missionLoader;
	public MissionLoadoutDataKeeper missionLoadoutDataKeeper;
	public PlaneDisplayControl planeDisplayControl;

	public void TestFlight()
	{
		missionLoadoutDataKeeper.ClearPlanesFromMissionLoadout();
		missionLoadoutDataKeeper.AddPlaneToMissionLoadout(planeDisplayControl.currentPlane.GetComponent<AircraftCore>().aircraftId);
		missionLoader.LoadTestFlightMission();
	}

	// Use this for initialization
	void Start () {
		missionLoader = GameObject.Find("MissionLoadoutDataKeeper").GetComponent<MissionLoader>();
		missionLoadoutDataKeeper = MissionLoadoutDataKeeper.missionLoadoutDataKeeper;
		planeDisplayControl = GameObject.Find("PlaneDisplayControl").GetComponent<PlaneDisplayControl>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
