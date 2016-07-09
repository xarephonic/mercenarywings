using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionLoadoutDataKeeper : MonoBehaviour {

	public static MissionLoadoutDataKeeper instance;

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
		if(instance == null)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
