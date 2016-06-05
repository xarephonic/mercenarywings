using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BottomBarControl : MonoBehaviour {

	public PlaneDisplayControl ps;

	public GameObject bottomBarPlaneButtonPrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < ps.planesArray.Length; i++) {

			GameObject x = Instantiate(bottomBarPlaneButtonPrefab,Vector3.zero,Quaternion.identity) as GameObject;

			x.transform.SetParent(gameObject.transform);

			x.GetComponent<Button>().image.sprite = ps.planesArray[i].GetComponent<AircraftCore>().aircraftPicture;

			x.transform.localScale = Vector3.one;

			int indexToGoTo = i;

			x.GetComponent<Button>().onClick.AddListener(() => ps.GoToIndex(indexToGoTo));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
