using UnityEngine;
using System.Collections;

public class TargetingConeHandler : MonoBehaviour {

	public GameObject fakePlane;

	public GameObject targetingConePrefab;

	public GameObject[] targetingCones;

	public bool show;

	public void SpawnTargetingConesForPlayer()
	{
		LoadoutHolder l = LoadoutHolder.loadoutHolder.GetComponent<LoadoutHolder>();

		targetingCones = new GameObject[l.mySpawnedPlanes.Count];

		for (int i = 0; i < targetingCones.Length; i++) {

			targetingCones[i] = Instantiate(targetingConePrefab,Vector3.zero,Quaternion.identity) as GameObject;
		}

		show = true;
	}

	public void StickToPlane()
	{
		LoadoutHolder l = LoadoutHolder.loadoutHolder.GetComponent<LoadoutHolder>();

		for (int i = 0; i < l.mySpawnedPlanes.Count; i++) {

			targetingCones[i].transform.SetParent(l.mySpawnedPlanes[i].transform);
			
			targetingCones[i].transform.localEulerAngles = Vector3.zero;
			
			targetingCones[i].transform.position = l.mySpawnedPlanes[i].transform.position + l.mySpawnedPlanes[i].transform.forward*2;

			targetingCones[i].transform.GetChild(0).GetComponent<ConeDetector>().myPlane = l.mySpawnedPlanes[i];
		}
	}

	public void StickToFakePlane()
	{
		foreach(GameObject cone in targetingCones)
		{
			cone.SetActive(false);
		}

		targetingCones[0].SetActive(true);

		targetingCones[0].transform.SetParent(fakePlane.transform);
		
		targetingCones[0].transform.localEulerAngles = Vector3.zero;
		
		targetingCones[0].transform.position = fakePlane.transform.position + fakePlane.transform.forward*2;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(show)
		{
			if(PlaneSelector.currentMode == PlaneSelector.PlayMode.commandMode)
			{
				StickToFakePlane();
			}
			else if(PlaneSelector.currentMode == PlaneSelector.PlayMode.playMode)
			{
				StickToPlane();
			}
		}
	}
}
