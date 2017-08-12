using UnityEngine;
using SimpleJSON;
using DataClasses;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Firebase;
using Firebase.Database;

//this class keeps all the data related to every asset in the game

public class AssetKeeper : MonoBehaviour {

    public static AssetKeeper instance;
	public Dictionary<int, PlaneVO> allPlanesDict = new Dictionary<int, PlaneVO>();
	public List<PlaneVO> allPlanes = new List<PlaneVO>();
    public List<PlaneVO> playerPlanes = new List<PlaneVO>();
    public Dictionary<int, PlaneVO> playerPlanesDict = new Dictionary<int, PlaneVO>();

	public void PlanesListToDict(List<PlaneVO> planeList, Dictionary<int, PlaneVO> planeDict) {
        Debug.Log("planes list to dict called");
        try {
            planeDict.Clear();
            foreach (var plane in planeList) {
                planeDict.Add(plane.id, plane);
            }
        } catch(System.Exception e)
        {
            Debug.Log(e);
        }
        Debug.Log("planes list to dict finished");
	}

	//TODO Add a method to encrypt this data
	public void PopulateStartData(){
		Debug.Log("populating start data");
		
		TextAsset allPlanesJson = Resources.Load("allPlanes") as TextAsset;
		string allPlanesPath = Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlanesLocal);
		Directory.CreateDirectory(allPlanesPath);
		FileStream fs = File.Create(Path.Combine(allPlanesPath,"allPlanes"));
		fs.Write(allPlanesJson.bytes,0,allPlanesJson.bytes.Length);
		fs.Close();

	}

	public void PopulatePlayerStartData(){
		Debug.Log("Populating player start data");

		TextAsset playerPlanesJson = Resources.Load("playerPlanes") as TextAsset;
		string playerPlanesPath = Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlayerPlanesLocal);
		Directory.CreateDirectory(playerPlanesPath);
		FileStream fs = File.Create(Path.Combine(playerPlanesPath,"playerPlanes"));
		fs.Write(playerPlanesJson.bytes,0,playerPlanesJson.bytes.Length);
		fs.Close();

	}

    public void GetAllDataFromDB (string userId)
    {
        Debug.Log("AssetKeeper start");

        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://mercenary-wings-94494733.firebaseio.com/");

        FirebaseDatabase.DefaultInstance
            .GetReference("planes")
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    allPlanes.Clear();
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot child in snapshot.Children)
                    {
                        PlaneVO pvo = JsonUtility.FromJson<PlaneVO>(child.GetRawJsonValue());

                        allPlanes.Add(pvo);
                    }

                    Debug.Log(allPlanes.Count);

                    PlanesListToDict(allPlanes, allPlanesDict);
                    GetPlayerDataFromDB(userId);
                }
            });

        //This creates the local files to start with. 
        //We have this because at least we want the game to be playable without additional downloads first
        if (!Directory.Exists(Path.Combine(Constants.inst.localDbUrl, Constants.inst.getAllPlanesLocal)))
            PopulateStartData();
        if (!Directory.Exists(Path.Combine(Constants.inst.localDbUrl, Constants.inst.getAllPlayerPlanesLocal)))
            PopulatePlayerStartData();
    }

    public void GetPlayerDataFromDB (string userId)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("playerPlanes/" + userId)
            .GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                        // Handle the error...
                    }
            else if (task.IsCompleted)
            {
                playerPlanes.Clear();
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot child in snapshot.Children)
                {
                    PlaneVO pvo = new PlaneVO();
                    allPlanesDict.TryGetValue(int.Parse(child.GetRawJsonValue()), out pvo);
                    playerPlanes.Add(pvo);
                }

                Debug.Log("player planes: " + playerPlanes.Count);
                //PlanesListToDict(playerPlanes, playerPlanesDict);
                MissionLoader.instance.LoadHangar();
            }
        });
    }

	void Awake()
    {
		Debug.Log("AssetKeeper awake");

		if(instance == null)
        {
			instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
    }
		
    void Start()
    {

    }
}
