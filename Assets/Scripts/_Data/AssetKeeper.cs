using UnityEngine;
using SimpleJSON;
using DataClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using AssetBundles;

//this class keeps all the data related to every asset in the game

public class AssetKeeper : MonoBehaviour {

    public static AssetKeeper instance;
	public List<PlaneVO> allPlanes = new List<PlaneVO>();
	public List<PlaneVO> playerPlanes = new List<PlaneVO>();

	public delegate void DataRetrieveAction (string data, RetrieveDataType t, bool remote);
	public event DataRetrieveAction OnDataRetrieveSuccess;
	public event DataRetrieveAction OnDataRetrieveError;

	public enum RetrieveDataType {
		ALL_PLANES,
		PLAYER_PLANES
	};

	string CreateUrl(RetrieveDataType t, bool remote) {
		switch (t) {
		case RetrieveDataType.ALL_PLANES:
			if(remote) {
				return Constants.inst.dbUrl + Constants.inst.getAllPlanesUrl;
			} else {
				return Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlanesLocal).ToString();
			}
			break;
		case RetrieveDataType.PLAYER_PLANES:
			if(remote) {
				return Constants.inst.dbUrl + Constants.inst.getUserUrl;
			} else {
				return Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlayerPlanesLocal);
			}
			break;
		default:
			return "Cannot create url for "+ t + "remote: "+ remote;
			break;
		};
	}

	void PutIntoList<T>(string s, List<T> targetList)
    {
		JSONClass jObj = JSON.Parse(s).AsObject;
		JSONArray jArr = jObj["planes"].AsArray;

        MethodInfo m = typeof(T).GetMethod("FromJson");

        foreach (JSONClass jClass in jArr)
        {
			string[] sArr = new string[1] { jClass.ToString() };

			var obj = m.Invoke(null, sArr);

            targetList.Add((T)obj);
        }
    }

	public void RetrieveData(RetrieveDataType t, bool remote, WWWForm postData = null) {
		string url = CreateUrl(t, remote);
		Debug.Log("Retrieving "+ t.ToString() + "remote: "+ remote.ToString()+" from: "+ url);
		if(remote){
			StartCoroutine(Retriever.RetrieveRemoteData(url,postData, delegate(string s) {
				if(s == "error") {
					OnDataRetrieveError(s, t, remote);
				} else {
					OnDataRetrieveSuccess(s, t, remote);
				}
			}));
		} else {
			StartCoroutine(Retriever.RetrieveLocalData(url,delegate(string s) {
				if(s == "error") {
					OnDataRetrieveError(s, t, remote);
				} else {
					OnDataRetrieveSuccess(s, t, remote);
				}
			}));
		}
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
		Debug.Log("AssetKeeper start");

		//This creates the local files to start with. 
		//We have this because at least we want the game to be playable without additional downloads first
		if(!Directory.Exists(Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlanesLocal)))
			PopulateStartData();
		if(!Directory.Exists(Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlayerPlanesLocal)))
			PopulatePlayerStartData();
		

		OnDataRetrieveError += (data, t, remote) => {
			if(remote) {
				Debug.Log("Failed remote data retrieve for "+ t);
				Debug.Log("Falling back to local file");
				RetrieveData(t,false);
			} else {
				Debug.Log("Failed local data retrieve for "+ t);
			}
		};

		OnDataRetrieveSuccess += (data, t, remote) => {
			Debug.Log("Retrieved data for "+ t+ "remote: "+remote);
			switch(t) {
			case RetrieveDataType.ALL_PLANES:
				PutIntoList(data, allPlanes);
				break;
			case RetrieveDataType.PLAYER_PLANES:
				PutIntoList(data,playerPlanes);
				break;
			default:
				Debug.Log("I dont know where to put "+ t);
				break;
			}
		};

		//we first try to get the data from remote
		RetrieveData(RetrieveDataType.ALL_PLANES, true);
		RetrieveData(RetrieveDataType.PLAYER_PLANES, true);

    }
}
