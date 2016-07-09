using UnityEngine;
using System.Collections;

public class CombatMissionSetupHandler : MonoBehaviour {

	public GameObject[] aircraftAssets;
	public GameObject playerSpawnPositionsRoot;
	public GameObject opponentSpawnPositionsRoot;
	public SceneAssetsKeeper sceneAssetsKeeper;

	public Vector3[] playerSpawnPositions;
	public Vector3[] opponentSpawnPositions;

	//TODO replace this with a system to select appropriate enemy planes
	public GameObject opponentPlanePrefab;

	public delegate void MissionSpawnAction();
	public static event MissionSpawnAction OnMissionAssetsSpawned;

	private int sceneAssetId;

	public void SetAssetId(GameObject g){
		g.AddComponent<AssetIdentifier>().sceneAssetId = sceneAssetId++;
	}

	void GetPlayerSpawnPositions()
	{
		playerSpawnPositions = new Vector3[playerSpawnPositionsRoot.transform.childCount];

		for (int i = 0; i < playerSpawnPositions.Length; i++) 
		{
			playerSpawnPositions[i] = playerSpawnPositionsRoot.transform.GetChild(i).transform.position;
		}
	}

	void GetOpponentSpawnPositions(){
		opponentSpawnPositions = new Vector3[opponentSpawnPositionsRoot.transform.childCount];

		for (int i = 0; i < opponentSpawnPositions.Length; i++) {
			opponentSpawnPositions[i] = opponentSpawnPositionsRoot.transform.GetChild(i).transform.position;
		}
	}

	void SpawnPlayerPlanes()
	{
		for (int i = 0; i < MissionLoadoutDataKeeper.instance.planesToTakeIntoMission.Count; i++) 
		{
			GameObject aircraftToBeSpawned = null;
			
			foreach(GameObject aircraftAsset in aircraftAssets)
			{
				if(aircraftAsset.GetComponent<AircraftCore>().aircraftId == MissionLoadoutDataKeeper.instance.planesToTakeIntoMission[i])
				{
					if(aircraftAsset.GetComponent<MovementModule>() != null){
						aircraftToBeSpawned = aircraftAsset;
						break;
					}
				}
			}
			
			GameObject x = Instantiate(aircraftToBeSpawned,playerSpawnPositions[i],Quaternion.identity) as GameObject;

			x.GetComponent<MovementModule>().airSpeed = x.GetComponent<MovementModule>().GetOptimalSpeed();

			SetAssetId(x);

			sceneAssetsKeeper.instantiatedAssets.Add(x);
			sceneAssetsKeeper.playerAssets.Add(x);
		}
	}

	void SpawnEnemyPlanes(){
		for (int i = 0; i < Random.Range(3,6); i++) {
			GameObject x = Instantiate(opponentPlanePrefab,opponentSpawnPositions[i],Quaternion.identity) as GameObject;

			x.GetComponent<MovementModule>().airSpeed = x.GetComponent<MovementModule>().GetOptimalSpeed();

			SetAssetId(x);

			sceneAssetsKeeper.instantiatedAssets.Add(x);
			sceneAssetsKeeper.opponentAssets.Add(x);
		}
	}

	// Use this for initialization
	void Start () {

		sceneAssetsKeeper = GetComponent<SceneAssetsKeeper>();

		aircraftAssets = AssetLoader.instance.loadedAssets.ToArray();

		GetPlayerSpawnPositions();
		SpawnPlayerPlanes();

		GetOpponentSpawnPositions();
		SpawnEnemyPlanes();

		OnMissionAssetsSpawned();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
