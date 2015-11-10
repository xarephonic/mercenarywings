using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionLoadoutDataKeeper : MonoBehaviour {

	public static MissionLoadoutDataKeeper missionLoadoutDataKeeper;

	public List<int> planesToTakeIntoMission = new List<int>();

	public void AddPlaneToMissionLoadout(int planeId)
	{
		planesToTakeIntoMission.Add(planeId);
	}

	public void RemovePlaneFromMissionLoadout(int planeId)
	{
		planesToTakeIntoMission.Remove(planeId);
	}

	public void ClearPlanesFromMissionLoadout()
	{
		planesToTakeIntoMission.Clear();
	}

	// Use this for initialization
	void Awake () {
		if(missionLoadoutDataKeeper == null)
		{
			missionLoadoutDataKeeper = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
