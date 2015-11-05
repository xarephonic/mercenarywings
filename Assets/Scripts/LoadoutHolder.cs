using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadoutHolder : MonoBehaviour {

	public static GameObject loadoutHolder;

	public planeswapper ps;

	public TargetingConeHandler t;

	public GameObject[] planes;

	public List<int> chosenPlanes = new List<int>();

	public List<PlaneMoveControl> mySpawnedPlanes = new List<PlaneMoveControl>();

	public void SwitchToTestFlight()
	{
		Application.LoadLevel("ucakmove");
	}

	public void ChoosePlane(int indexNumberOfChosenPlane)
	{
		chosenPlanes.Add(indexNumberOfChosenPlane);
	}

	public void RemovePlaneFromSetup(int indexNumberOfChosenPlane)
	{
		chosenPlanes.Remove(indexNumberOfChosenPlane);
	}

	public void ResetChosenPlanes()
	{
		chosenPlanes.Clear();
	}

	public void SetupMission()
	{
		GameObject spawn = GameObject.Find("SpawnPositions");

		List<Vector3> spawnPositions = new List<Vector3>();

		for (int i = 0; i < spawn.transform.childCount; i++) {

			spawnPositions.Add(spawn.transform.GetChild(i).position);
		}

		for (int i = 0; i < chosenPlanes.Count; i++) {

			int spawnPos = Random.Range(0,spawnPositions.Count);

			GameObject x = Instantiate(planes[chosenPlanes[i]],spawnPositions[spawnPos],Quaternion.identity) as GameObject;
			
			mySpawnedPlanes.Add(x.GetComponent<PlaneMoveControl>());
			
			x.tag = "Player";
			
			//x.transform.eulerAngles = Vector3.forward;

			spawnPositions.RemoveAt(spawnPos);
		}

		t.SpawnTargetingConesForPlayer();

	}

	void Awake()
	{
		if(LoadoutHolder.loadoutHolder == null)
		{
			LoadoutHolder.loadoutHolder = this.gameObject;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void OnLevelWasLoaded()
	{
		t = GameObject.Find("TargetingConesHandler").GetComponent<TargetingConeHandler>();

		if(Application.loadedLevelName == "ucakmove")
		{
			SetupMission();
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}
}
