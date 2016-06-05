using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridManager : MonoBehaviour {

	public GameObject gridPrefab;

	public GameObject grid;

	public Button showGridButton;

	public void ToggleGrid(){
		if(grid.activeInHierarchy)
			grid.SetActive(false);
		else
			grid.SetActive(true);
	}

	public void SetGridPosition(GameObject plane){
		grid.transform.position = plane.transform.position - new Vector3(0,1,0);
	}

	// Use this for initialization
	void Start () {

		grid = Instantiate(gridPrefab,Vector3.zero,Quaternion.identity) as GameObject;

		//TODO fix grid duplicate
		//we need the grid to be seen from both sides so we're creating a duplicate upside down. Please find a better way to do this
		GameObject gridUpsideDown = Instantiate(gridPrefab,Vector3.zero,Quaternion.identity) as GameObject;

		gridUpsideDown.transform.eulerAngles = new Vector3(180,0,0);

		gridUpsideDown.transform.SetParent(grid.transform);

		grid.SetActive(false);

		PlayerPlaneSelectionHandler.OnSelectedPlaneChanged += SetGridPosition;

		showGridButton = GameObject.Find("ShowGridButton").GetComponent<Button>();

		showGridButton.onClick.AddListener(delegate {
			ToggleGrid();
		});
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
