using UnityEngine;
using SimpleJSON;
using DataClasses;
using System.Collections;
using System.Collections.Generic;

//this class keeps all the data related to every asset in the game
public class AssetKeeper : MonoBehaviour {

    public static AssetKeeper assetKeeper;
    public List<PlaneVO> allPlanes = new List<PlaneVO>(); 

    void PutIntoAllPlanesArray(string s) {
        JSONArray jArr = JSON.Parse(s).AsArray;
        
        foreach (JSONClass jClass in jArr)
        {
            PlaneVO pvo = PlaneVO.FromJson(jClass);

            allPlanes.Add(pvo);
        }
        
    }

    public void RetrieveAllPlaneData()
    {
        StartCoroutine(Retriever.RetrieveData(Constants.constants.dbUrl + Constants.constants.getAllPlanesUrl,null,PutIntoAllPlanesArray));
    }  

    void Awake()
    {
        if(assetKeeper == null)
        {
            assetKeeper = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    void Start()
    {
        RetrieveAllPlaneData();
    }
}
