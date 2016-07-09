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

    void PutIntoList<T>(string s, List<T> targetList)
    {
        JSONArray jArr = JSON.Parse(s).AsArray;

        MethodInfo m = typeof(T).GetMethod("FromJson");

        foreach (JSONClass jClass in jArr)
        {
            var obj = m.Invoke(null,new JSONClass[1] { jClass });

            targetList.Add((T)obj);
        }
    }

    public void RetrieveAllPlaneData()
    {
		StartCoroutine(Retriever.RetrieveData(Constants.inst.dbUrl + Constants.inst.getAllPlanesUrl,null,delegate(string s)
        {
			if(s == "error"){
				StartCoroutine(Retriever.RetrieveData(Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlanesLocal),delegate(string l) {
						PutIntoList(l,allPlanes);
				}));
			}else {
				PutIntoList(s, allPlanes);
			}
		}));
    }

	public void RetrievePlayerPlaneData(){
		StartCoroutine(Retriever.RetrieveData(Constants.inst.dbUrl + Constants.inst.getUserUrl, null, delegate(string s){
			if(s == "error"){
				StartCoroutine(Retriever.RetrieveData(Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlayerPlanesLocal), delegate(string l) {
					JSONArray playerPlaneIds = JSON.Parse(l).AsArray;
					foreach(JSONData id in playerPlaneIds){
						playerPlanes.Add(allPlanes.Find(x => x.id == id.AsInt));
					}
				}));
			}else {
				PutIntoList(s,playerPlanes);	
			}

		}));
	}

	//TODO Add a method to encrypt this data
	public void PopulateStartData(){
		
		TextAsset allPlanesJson = Resources.Load("allPlanes") as TextAsset;
		string allPlanesPath = Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlanesLocal);
		Directory.CreateDirectory(allPlanesPath);
		FileStream fs = File.Create(Path.Combine(allPlanesPath,"allPlanes"));
		fs.Write(allPlanesJson.bytes,0,allPlanesJson.bytes.Length);
		fs.Close();
	}

	public void PopulatePlayerStartData(){
		TextAsset playerPlanesJson = Resources.Load("playerPlanes") as TextAsset;
		string playerPlanesPath = Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlayerPlanesLocal);
		Directory.CreateDirectory(playerPlanesPath);
		FileStream fs = File.Create(Path.Combine(playerPlanesPath,"playerPlanes"));
		fs.Write(playerPlanesJson.bytes,0,playerPlanesJson.bytes.Length);
		fs.Close();
	}

	void Awake()
    {
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
		if(!Directory.Exists(Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlanesLocal)))
			PopulateStartData();
		if(!Directory.Exists(Path.Combine(Constants.inst.localDbUrl,Constants.inst.getAllPlayerPlanesLocal)))
			PopulatePlayerStartData();
		
        RetrieveAllPlaneData();
		RetrievePlayerPlaneData();
    }
}
