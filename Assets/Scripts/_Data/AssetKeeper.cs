using UnityEngine;
using SimpleJSON;
using DataClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AssetBundles;

//this class keeps all the data related to every asset in the game
public class AssetKeeper : MonoBehaviour {

    public static AssetKeeper assetKeeper;
    public List<PlaneVO> allPlanes = new List<PlaneVO>();

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
        StartCoroutine(Retriever.RetrieveData(Constants.constants.dbUrl + Constants.constants.getAllPlanesUrl,null,delegate(string s)
        {
            PutIntoList(s, allPlanes);
        }));
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
