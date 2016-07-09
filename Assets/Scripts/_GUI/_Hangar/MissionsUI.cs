using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DataClasses;

public class MissionsUI : MonoBehaviour {

	public Image contentArea;
	public Image viewPortArea;
	public Scrollbar scroll;

	public GameObject missionEntryPrefab;

	public Button missionsButton;

	public GameObject missionLoadoutMenu;
	public GameObject missionSelectionMenu;

	public List<GameObject> missionEntries = new List<GameObject>();

	IEnumerator timeout(){
		yield return new WaitForEndOfFrame();

		if(contentArea.rectTransform.sizeDelta.y > viewPortArea.rectTransform.sizeDelta.y)
			scroll.gameObject.SetActive(true);
		else
			scroll.gameObject.SetActive(false);	
	}

	void ResetScrollBarPos(){
		scroll.value = 1;
	}

	public void GenerateMissionEntries(){
		foreach(MissionVO mission in MissionManager.instance.missions){
			GameObject missionEntry = Instantiate(missionEntryPrefab,Vector3.zero,Quaternion.identity) as GameObject;

			missionEntries.Add(missionEntry);

			missionEntry.transform.SetParent(contentArea.transform);
			missionEntry.transform.localScale = Vector3.one;

			missionEntry.GetComponent<MissionEntryUI>().Init(mission.name,mission.id);
		}
	}

	// Use this for initialization
	void Awake () {
		MissionManager.OnMissionsGenerated += GenerateMissionEntries;
		MissionManager.OnMissionSelected += delegate(MissionVO mission) {
			missionLoadoutMenu.SetActive(true);
			missionSelectionMenu.SetActive(false);
		};
	}

	void Start () {

		missionSelectionMenu.SetActive(false);
		missionLoadoutMenu.SetActive(false);

		missionsButton = GameObject.Find("MissionsButton").GetComponent<Button>();

		missionsButton.onClick.AddListener(delegate {
			missionSelectionMenu.SetActive(true);
			missionLoadoutMenu.SetActive(false);

			StartCoroutine(timeout());
			ResetScrollBarPos();
		});


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
