using UnityEngine;
using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using DataClasses;

//this class 
//loads/unloads assets
//downloads and caches assets
//creates the asset in prefab form when loading into memory

public class AssetLoader : MonoBehaviour {

	public static AssetLoader instance;

	public enum PlaneAssetType {
		HANGAR,
		INFLIGHT,
		NONE
	}

	public void LoadPlayerAssets () {
		Debug.Log("Starting loading of player assets");
		foreach (PlaneVO planeVo in AssetKeeper.instance.playerPlanes) {
			string planeHangarUrl = planeVo.hangarAssetUrl;
			string planeInFlightUrl = planeVo.inFlightAssetUrl;

			StartCoroutine(
				LoadAsset(planeHangarUrl, planeVo.name, planeVo.assetVersion, planeVo.id,
					(GameObject g, int assetId) => {
						planeVo.hangarAsset = g;
						planeVo.ToHangarPlane(planeVo.hangarAsset);
					}
				)
			);
			StartCoroutine(
				LoadAsset(planeInFlightUrl, planeVo.name, planeVo.assetVersion, planeVo.id, 
					(GameObject g, int assetId) => {
						planeVo.inFlightAsset = g;
						planeVo.ToPlane(planeVo.inFlightAsset);
					}
				)
			);
		}
	}

	public IEnumerator LoadAsset(string assetUrl, string assetName, int version, int assetId, AssetLoadCallBack callback = null) {
		Debug.Log("Loading asset "+assetName+" from "+assetUrl+" of version "+version);
		WWW w = WWW.LoadFromCacheOrDownload(assetUrl,version);

		yield return w;

		if(!string.IsNullOrEmpty(w.error)) {
			Debug.Log(assetUrl+" with name "+assetName+" ver: "+version+" load error: "+w.error);
		} else {
			Debug.Log("got the asset from: "+assetUrl);
			string[] names = w.assetBundle.GetAllAssetNames();
			GameObject asset = w.assetBundle.LoadAsset<GameObject>(names[0]);
			loadedAssets.Add(asset);
			callback(asset, assetId);
		}
	}


	public delegate void AssetLoadCallBack(GameObject g, int assetId);

    public List<GameObject> loadedAssets = new List<GameObject>();

	void Start () {
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
