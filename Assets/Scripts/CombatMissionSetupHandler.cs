using UnityEngine;
using System.Collections;

public class CombatMissionSetupHandler : MonoBehaviour {

	public GameObject[] aircraftAssets;
	public GameObject playerSpawnPositionsRoot;
	public SceneAssetsKeeper sceneAssetsKeeper;

	public Vector3[] spawnPositions;

	void GetSpawnPositions()
	{
		spawnPositions = new Vector3[playerSpawnPositionsRoot.transform.childCount];

		for (int i = 0; i < spawnPositions.Length; i++) 
		{
			spawnPositions[i] = playerSpawnPositionsRoot.transform.GetChild(i).transform.position;
		}
	}

	void SpawnPlayerPlanes()
	{
		for (int i = 0; i < MissionLoadoutDataKeeper.missionLoadoutDataKeeper.planesToTakeIntoMission.Count; i++) 
		{
			GameObject aircraftToBeSpawned = null;
			
			foreach(GameObject aircraftAsset in aircraftAssets)
			{
				if(aircraftAsset.GetComponent<AircraftCore>().aircraftId == MissionLoadoutDataKeeper.missionLoadoutDataKeeper.planesToTakeIntoMission[i])
				{
					aircraftToBeSpawned = aircraftAsset;
					break;
				}
			}
			
			GameObject x = Instantiate(aircraftToBeSpawned,spawnPositions[i],Quaternion.identity) as GameObject;

			x.GetComponent<MovementModule>().airSpeed = x.GetComponent<MovementModule>().GetOptimalSpeed();

			sceneAssetsKeeper.instantiatedAssets.Add(x);
			sceneAssetsKeeper.playerAssets.Add(x);
		}

		CombatSelectionHandler.selectedObject = sceneAssetsKeeper.playerAssets[0];
	}

	// Use this for initialization
	void Start () {

		sceneAssetsKeeper = GetComponent<SceneAssetsKeeper>();

		GetSpawnPositions();
		SpawnPlayerPlanes();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
