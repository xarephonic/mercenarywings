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

	public delegate void PlaneSelectionAction(PlaneVO plane);

	public event PlaneSelectionAction OnPlaneSelectionComplete;

	// Use this for initialization
	void Start () {

		foreach(PlaneVO plane in AssetKeeper.instance.playerPlanes){
			GameObject entry = Instantiate(selectablePlaneEntryPrefab,Vector3.zero,Quaternion.identity) as GameObject;
            entry.GetComponent<PlaneSelectionEntry>().planeImage.GetComponent<Image>().sprite = plane.hangarPicture;
            entry.GetComponent<PlaneSelectionEntry>().planeText.GetComponent<Text>().text = plane.name;
			entry.GetComponent<PlaneSelectionEntry>().selectButton.GetComponent<Button>().onClick.AddListener(delegate {
				OnPlaneSelectionComplete(plane);	
			});

			selectablePlaneEntries.Add(entry);

			entry.transform.SetParent(contentArea.transform);
		}

		closeBttn.onClick.AddListener(delegate {
            gameObject.SetActive(false);	
		});

        OnPlaneSelectionComplete += delegate (PlaneVO pvo)
        {
            gameObject.SetActive(false);
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
