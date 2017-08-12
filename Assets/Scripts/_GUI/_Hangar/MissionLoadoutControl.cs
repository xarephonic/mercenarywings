using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DataClasses;

public class MissionLoadoutControl : MonoBehaviour {

	public MissionLoader missionLoader;
	public MissionLoadoutDataKeeper missionLoadoutDataKeeper;
	public HangarPlaneDisplayControl planeDisplayControl;
	public PlaneSelectionUI planeSelectionMenu;

	[System.Serializable]
	public class PlaneEntry {
		public GameObject planePicHolder;
		public Image planeImg;
		public Text planeName;
		public GameObject pilotPicHolder;
		public Image pilotImg;
		public Text pilotName;
		public Button changePlaneBttn;
		public Button changeLoadoutBttn;

		public Button addPlaneBttn;

		public PlaneEntry(){
			
		}
	}

	public PlaneEntry[] entries = new PlaneEntry[4];

	public int selectedPlaneEntry;

	public void ChangePlaneEntry(PlaneVO pvo){
		PlaneEntry entry = entries[selectedPlaneEntry];

        entry.planeImg.sprite = pvo.hangarPicture;
        entry.planeName.text = pvo.name;
		//TODO set pilot image
		//TODO set pilot name

		entry.changePlaneBttn.gameObject.SetActive(true);
		entry.changeLoadoutBttn.gameObject.SetActive(true);
		entry.planePicHolder.gameObject.SetActive(true);
		entry.pilotPicHolder.gameObject.SetActive(true);

		entry.addPlaneBttn.gameObject.SetActive(false);
	}

	public void ResetPlaneEntry(int entryIndex){
		PlaneEntry entry = entries[entryIndex];

		entry.planePicHolder.SetActive(false);
		entry.pilotPicHolder.SetActive(false);
		entry.changePlaneBttn.gameObject.SetActive(false);
		entry.changeLoadoutBttn.gameObject.SetActive(false);

		entry.addPlaneBttn.gameObject.SetActive(true);
	}

	public void OpenPlaneSelectionMenu(int selectedEntryIndex)
	{
		selectedPlaneEntry = selectedEntryIndex;
		planeSelectionMenu.gameObject.SetActive(true);
	}

	public void CloseMissionLoadoutMenu(){
        missionLoadoutDataKeeper.planesToTakeIntoMission.Clear();

		for (int i = 0; i < entries.Length; i++) {
			ResetPlaneEntry(i);
		}
	}

	public void LaunchMission(){
		missionLoader.LoadTestFlightMission();
	}

	public void TestFlight()
	{        
		missionLoadoutDataKeeper.planesToTakeIntoMission.Clear();
        missionLoadoutDataKeeper.planesToTakeIntoMission.Add(AssetKeeper.instance.playerPlanes[planeDisplayControl.displayedPlaneIndex]);
		missionLoader.LoadTestFlightMission();
	}

	// Use this for initialization
	void Start () {
		missionLoader = GameObject.FindObjectOfType<MissionLoader>();
		//missionLoader = GameObject.Find("MissionLoadoutDataKeeper").GetComponent<MissionLoader>();
		missionLoadoutDataKeeper = MissionLoadoutDataKeeper.instance;
		planeDisplayControl = GameObject.Find("PlaneDisplayControl").GetComponent<HangarPlaneDisplayControl>();

        planeSelectionMenu.OnPlaneSelectionComplete += delegate (PlaneVO plane)
        {
            missionLoadoutDataKeeper.planesToTakeIntoMission.Add(plane);
            ChangePlaneEntry(plane);
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
