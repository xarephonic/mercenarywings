using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionEntryUI : MonoBehaviour {

	public int id;
	public Text missionName;
	public Button missionButton;

	public void Init(string name, int missionId){
		missionName.text = name;
		this.id = missionId;

		missionButton.onClick.AddListener(delegate {
			MissionManager.instance.SelectMission(this.id);

		});
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
