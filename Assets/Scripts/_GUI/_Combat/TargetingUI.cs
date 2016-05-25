using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TargetingUI : MonoBehaviour {

	public GameObject canvas;

	public GameObject targetIndicatorPrefab;

	public List<GameObject> targetIndicatorPool = new List<GameObject>();
	public List<GameObject> targets = new List<GameObject>();

	public bool show;

	public void ToggleShow(){
		show = !show;
	}

	public void ShowTargetIndicators(){
		for (int i = 0; i < SceneAssetsKeeper.instance.opponentAssets.Count; i++) {
			GameObject target = SceneAssetsKeeper.instance.opponentAssets[i];

			Vector2 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

			screenPos = new Vector2(screenPos.x / canvas.transform.localScale.x , screenPos.y / canvas.transform.localScale.y);

			targetIndicatorPool[i].GetComponent<RectTransform>().anchoredPosition = screenPos;

			targetIndicatorPool[i].SetActive(true);
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

			targetIndicator.SetActive(false);

			targetIndicatorPool.Add(targetIndicator);

			BindIndicatorToTarget(targetIndicator.GetComponent<Button>(),target);
		}

	}

	// Use this for initialization
	void Start () {

		CombatMissionSetupHandler.OnMissionAssetsSpawned += CreateTargetIndicators;
	}
	
	// Update is called once per frame
	void Update () {
		if(show)
			ShowTargetIndicators();
		else
			HideTargetIndicators();
	}
}
