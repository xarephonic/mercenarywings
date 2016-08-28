using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DataClasses;

public class PlaneDisplayControl : MonoBehaviour {

	public List<GameObject> planesArray;

	public int planesArrayIndex;

	public AircraftCore currentPlane;

	public GameObject DisplayedPlanesParent;

	public void GoToIndex(int i)
	{
		planesArrayIndex = Mathf.Clamp(i,0,planesArray.Count-1);

		for (int j = 0; j < planesArray.Count; j++) {
			planesArray[j].SetActive(false);
		}

		planesArray[planesArrayIndex].SetActive(true);
		
		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	void OnLevelWasLoaded () {

		planesArray = new List<GameObject>();

		foreach(PlaneVO plane in AssetKeeper.instance.playerPlanes){
			Debug.Log(AssetKeeper.instance.playerPlanes.Count);
			Debug.Log(plane);
			StartCoroutine(AssetLoader.instance.GetAsset(plane.id,AssetLoader.PlaneAssetType.HANGAR,delegate(GameObject g){
				GameObject x = Instantiate(g,Vector3.zero,Quaternion.identity) as GameObject;
				x.transform.SetParent(DisplayedPlanesParent.transform);
				x.transform.localPosition = x.GetComponent<HangarDetails>().hangarPos;
				x.transform.localEulerAngles = Vector3.zero;
				planesArray.Add(x);	
				x.SetActive(false);
			}));
		}

		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
		currentPlane.gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () {

       

	}
	
	// Update is called once per frame

    void Update () {


	}
}
