using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DataClasses;

public class PlaneSelectionUI : MonoBehaviour {

	public GameObject selectablePlaneEntryPrefab;

	public List<GameObject> selectablePlaneEntries = new List<GameObject>();

	public Image contentArea;
	public Image viewPortArea;

	public Button closeBttn;

	public delegate void PlaneSelectionAction(int id);

	public static event PlaneSelectionAction OnPlaneSelectionComplete;

	// Use this for initialization
	void Start () {
		List<GameObject> planesArray = GameObject.Find("PlaneDisplayControl").GetComponent<PlaneDisplayControl>().planesArray;

		foreach(GameObject plane in planesArray){
			GameObject entry = Instantiate(selectablePlaneEntryPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			entry.GetComponent<PlaneSelectionEntry>().planeImage.GetComponent<Image>().sprite = plane.GetComponent<AircraftCore>().aircraftPicture;
			entry.GetComponent<PlaneSelectionEntry>().planeText.GetComponent<Text>().text = plane.GetComponent<AircraftCore>().aircraftName;
			int id = plane.GetComponent<AircraftCore>().aircraftId;
			entry.GetComponent<PlaneSelectionEntry>().selectButton.GetComponent<Button>().onClick.AddListener(delegate {
				OnPlaneSelectionComplete(id);	
			});

			selectablePlaneEntries.Add(entry);

			entry.transform.SetParent(contentArea.transform);
		}

		closeBttn.onClick.AddListener(delegate {
			OnPlaneSelectionComplete(-1);	
		});

		OnPlaneSelectionComplete += delegate(int id) {
			gameObject.SetActive(false);
		};		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
