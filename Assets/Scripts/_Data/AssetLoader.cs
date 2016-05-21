using UnityEngine;
using AssetBundles;
using System.Collections;
using System.Collections.Generic;

public class AssetLoader : MonoBehaviour {

    public List<GameObject> loadedAssets = new List<GameObject>();

	/*
	// Use this for initialization
	public IEnumerator Start () {
		
        AssetBundleManager.SetSourceAssetBundleURL("http://46.101.109.169/assets/");

        yield return StartCoroutine(AssetBundleManager.Initialize());

        StartCoroutine(GetAsset("eurofighter", "EuroFighter", "planes", "1"));

    }
	*/

    public IEnumerator GetAsset(string bundleName, string assetName, string assetType, string assetId)
    {
        AssetBundleManager.SetSourceAssetBundleURL("http://46.101.109.169/assets/"+assetType+"/"+assetId+"/");

        AssetBundleLoadAssetOperation operation = AssetBundleManager.LoadAssetAsync(bundleName, assetName, typeof(GameObject));

        yield return operation;

        loadedAssets.Add(operation.GetAsset<GameObject>());
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
