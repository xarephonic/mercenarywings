using UnityEngine;
using System.Collections;
using DataClasses;

public class CombatMissionSetupHandler : MonoBehaviour {

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
		Debug.Log("Set asset id "+sceneAssetId+" for "+g.name);
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
			PlaneVO pvo = MissionLoadoutDataKeeper.instance.planesToTakeIntoMission [i];

			StartCoroutine(AssetLoader.instance.LoadAsset(pvo.inFlightAssetUrl, pvo.name, pvo.assetVersion, pvo.inFlightAssetId, delegate(GameObject g, int assetId) {
				GameObject x = Instantiate(g, playerSpawnPositions[i], Quaternion.identity) as GameObject;
				pvo.ToPlane(x);

				x.GetComponent<MovementModule>().airSpeed = x.GetComponent<MovementModule>().GetOptimalSpeed();

				x.transform.localScale *= Constants.scaleFactor;

				SetAssetId(x);

				sceneAssetsKeeper.instantiatedAssets.Add(x);
				sceneAssetsKeeper.playerAssets.Add(x);
			}));
		}
	}

	void SpawnEnemyPlanes(){
		for (int i = 0; i < Random.Range(3,6); i++) {

			Debug.Log("Instantiating "+opponentPlanePrefab.name+"...");

			GameObject x = Instantiate(opponentPlanePrefab,opponentSpawnPositions[i],Quaternion.identity) as GameObject;

			Debug.Log("Instantiated "+opponentPlanePrefab.name+"!");

			x.GetComponent<MovementModule>().airSpeed = x.GetComponent<MovementModule>().GetOptimalSpeed();

			x.transform.localScale *= Constants.scaleFactor;

			SetAssetId(x);

			sceneAssetsKeeper.instantiatedAssets.Add(x);
			sceneAssetsKeeper.opponentAssets.Add(x);
		}
	}

	// Use this for initialization
	void Start () {

		OnMissionAssetsSpawned += () => Debug.Log("Mission Assets Spawned!");

		sceneAssetsKeeper = GetComponent<SceneAssetsKeeper>();

		//aircraftAssets = AssetLoader.instance.loadedAssets.ToArray();

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
