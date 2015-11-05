using UnityEngine;
using System.Collections;

public class AircraftLoadout : MonoBehaviour {

	public GameObject hardPointsRoot;

	public Vector3[] hardPointPositions;

	public GameObject[] loadedArmament;

	public void GetHardPointPositions()
	{
		hardPointPositions = new Vector3[hardPointsRoot.transform.childCount];

		for (int i = 0; i < hardPointPositions.Length; i++) 
		{
			hardPointPositions[i] = hardPointsRoot.transform.GetChild(i).transform.position;
		}
	}

	public void ClearLoadout()
	{
		for (int i = 0; i < loadedArmament.Length; i++) {
			loadedArmament[i] = null;
		}
	}

	public void ChangeItemAtPoint(int indexOfItemToBeChanged,GameObject newItem)
	{
		loadedArmament[indexOfItemToBeChanged] = newItem;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
