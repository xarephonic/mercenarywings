using UnityEngine;
using System.Collections;

public class AircraftLoadout : MonoBehaviour {

	public GameObject hardPointsRoot;

	public Vector3[] hardPointPositions;

	public GameObject[] loadedArmament;

	public GameObject testMissile;

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

	private void PopulateWithTestMissiles(){

		loadedArmament = new GameObject[hardPointPositions.Length];

		for (int i = 0; i < hardPointPositions.Length; i++) {
			GameObject testMis = Instantiate(testMissile , hardPointPositions[i], Quaternion.identity) as GameObject;

			testMis.transform.localScale *= Constants.scaleFactor;

			testMis.transform.SetParent(gameObject.transform);

			loadedArmament[i] = testMis;
		}
	}

	// Use this for initialization
	void Start () {
		if(hardPointsRoot)
			GetHardPointPositions();

		PopulateWithTestMissiles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
