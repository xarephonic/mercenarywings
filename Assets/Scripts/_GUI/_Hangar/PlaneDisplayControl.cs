using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaneDisplayControl : MonoBehaviour {

	public GameObject[] planesArray;

	public int planesArrayIndex;

	public AircraftCore currentPlane;

	public void Forward()
	{
		planesArrayIndex++;

		if(planesArrayIndex == planesArray.Length)
			planesArrayIndex = 0;

		for (int i = 0; i < planesArray.Length; i++) {
			planesArray[i].SetActive(false);
		}

		planesArray[planesArrayIndex].SetActive(true);

		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	public void Back()
	{
		planesArrayIndex--;

		if(planesArrayIndex == -1)
			planesArrayIndex = planesArray.Length-1;

		for (int i = 0; i < planesArray.Length; i++) {
			planesArray[i].SetActive(false);
		}
		
		planesArray[planesArrayIndex].SetActive(true);

		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	public void GoToIndex(int i)
	{
		planesArrayIndex = Mathf.Clamp(i,0,planesArray.Length-1);

		for (int j = 0; j < planesArray.Length; j++) {
			planesArray[j].SetActive(false);
		}

		planesArray[planesArrayIndex].SetActive(true);
		
		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	// Use this for initialization
	void Start () {

        currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();

	}
	
	// Update is called once per frame

    void Update () {


	}
}
