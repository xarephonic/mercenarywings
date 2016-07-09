using UnityEngine;
using DataClasses;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour {

	public static MissionManager instance;

	public int selectedMissionId;

	public List<MissionVO> missions = new List<MissionVO>();

	public delegate void MissionGenerationAction ();

	public static event MissionGenerationAction OnMissionsGenerated;

	public delegate void MissionSelectionAction (MissionVO mission);

	public static event MissionSelectionAction OnMissionSelected;

	public void SelectMission(int id){
		selectedMissionId = id;

		MissionVO mission = missions.Find(delegate(MissionVO obj) {
			return obj.id == selectedMissionId;
		});


		OnMissionSelected (mission);
	}


	//mock
	void GenerateRandomMissions(){
		for (int i = 0; i < Random.Range(2,20); i++) {
			int waves = Random.Range(1,5);

			MissionVO mis = new MissionVO(i,"Mission"+i,MissionType.DESTROY,waves);

			missions.Add(mis);
		}

		OnMissionsGenerated();
	}

	// Use this for initialization
	void Start () {

		instance = this;

		GenerateRandomMissions();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
