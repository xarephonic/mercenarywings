using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TargetingUI : MonoBehaviour {

	public GameObject canvas;

	public GameObject targetIndicatorPrefab;

	public List<GameObject> targetIndicatorPool = new List<GameObject>();

	public bool show;

	public void ToggleShow(){
		show = !show;
	}

	public void ShowTargetIndicators(){
		for (int i = 0; i < SceneAssetsKeeper.instance.opponentAssets.Count; i++) {
			GameObject target = SceneAssetsKeeper.instance.opponentAssets[i];

			if(target.activeSelf){
				Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

				if(screenPos.z > 0)
				{
					screenPos = new Vector3(screenPos.x / canvas.transform.localScale.x , screenPos.y / canvas.transform.localScale.y,0);
				}
				else
				{
					targetIndicatorPool[i].SetActive(false);
					return;
				}

				targetIndicatorPool[i].GetComponent<RectTransform>().anchoredPosition = screenPos;
				targetIndicatorPool[i].transform.GetChild(0).GetComponent<Text>().text = target.GetComponent<AircraftCore>().aircraftName;
				targetIndicatorPool[i].transform.GetChild(1).GetComponent<Text>().text = Vector3.Distance(PlayerPlaneSelectionHandler.selectedPlane.transform.position,target.transform.position).ToString(0+"m");

				targetIndicatorPool[i].SetActive(true);
			}else {
				targetIndicatorPool[i].SetActive(false);
			}
		}
	}

	public void HideTargetIndicators(){
		foreach(GameObject indicator in targetIndicatorPool){
			indicator.SetActive(false);
		}
	}

	public void BindIndicatorToTarget(Button indicator, GameObject target){
		
			indicator.onClick.AddListener(delegate{

			TargetSetter.SetTargetForSelectedPlane(target);
			});
	}

	public void CreateTargetIndicators(){
		foreach(GameObject target in SceneAssetsKeeper.instance.opponentAssets){

			GameObject targetIndicator = Instantiate(targetIndicatorPrefab,Vector3.zero,Quaternion.identity) as GameObject;

			targetIndicator.transform.SetParent(canvas.transform);

			targetIndicator.transform.SetAsFirstSibling();

			targetIndicator.SetActive(false);

			targetIndicator.GetComponent<Image>().color = Color.green;

			targetIndicatorPool.Add(targetIndicator);

			BindIndicatorToTarget(targetIndicator.GetComponent<Button>(),target);
		}

	}

	// Use this for initialization
	void Start () {

		CombatMissionSetupHandler.OnMissionAssetsSpawned += CreateTargetIndicators;

		SceneAssetsKeeper.OnAssetDestroyed += delegate(GameObject asset) {
			HideTargetIndicators();
			ShowTargetIndicators();
		};

		ToggleShow();
	}
	
	// Update is called once per frame
	void Update () {
		if(show)
			ShowTargetIndicators();
		else
			HideTargetIndicators();
	}
}
