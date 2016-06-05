using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerPlaneSelectionController : MonoBehaviour {

	public GameObject planeButtonPrefab;
	public GameObject planeButtonHighlightPrefab;

	public List<GameObject> planeButtons = new List<GameObject>();
	public GameObject highlight;

	void GeneratePlaneSelectionButtons(){
		foreach(GameObject plane in SceneAssetsKeeper.instance.playerAssets){
			GameObject planeButton = Instantiate(planeButtonPrefab,Vector3.zero,Quaternion.identity) as GameObject;

			planeButton.transform.SetParent(gameObject.transform);

			planeButton.GetComponent<Image>().sprite = plane.GetComponent<AircraftCore>().aircraftPicture;

			planeButton.transform.localScale = Vector3.one;

			planeButton.name = plane.GetComponent<AssetIdentifier>().sceneAssetId.ToString();

			planeButton.GetComponent<Button>().onClick.AddListener(delegate {
				PlayerPlaneSelectionHandler.SetSelectedPlane(SceneAssetsKeeper.instance.GetAssetById(int.Parse(planeButton.name)));
			});

			planeButtons.Add(planeButton);
		}

		highlight = Instantiate(planeButtonHighlightPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		highlight.transform.localScale = Vector3.one;

		highlight.transform.SetParent(gameObject.transform);

		StartCoroutine(timeout());
	}

	IEnumerator timeout(){
		yield return new WaitForEndOfFrame();

		SetHighlightPosition(PlayerPlaneSelectionHandler.selectedPlane);
	}

	void SetHighlightPosition(GameObject plane){
		if(planeButtons.Count <= 0)
			return;

		Vector3 pos = planeButtons.Find(delegate(GameObject obj) {
			return obj.name == plane.GetComponent<AssetIdentifier>().sceneAssetId.ToString();	
		}).transform.position;

		highlight.transform.position = pos;
		highlight.transform.SetAsFirstSibling();
		highlight.transform.localScale = Vector3.one;
		Vector2 size = planeButtons[0].GetComponent<RectTransform>().sizeDelta;
		highlight.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x+5,size.y+5);
	}

	void SetCommandToggles(){
		foreach (GameObject planeButton in planeButtons) {
			planeButton.transform.GetChild(0).GetComponent<Toggle>().isOn = SceneAssetsKeeper.instance.GetAssetById(int.Parse(planeButton.name)).GetComponent<MovementModule>().hasCommands();
		}
	}

	// Use this for initialization
	void Start () {
		CombatMissionSetupHandler.OnMissionAssetsSpawned += GeneratePlaneSelectionButtons;
		PlayerPlaneSelectionHandler.OnSelectedPlaneChanged += SetHighlightPosition;
	}
	
	// Update is called once per frame
	void Update () {
		SetCommandToggles();
	}
}
