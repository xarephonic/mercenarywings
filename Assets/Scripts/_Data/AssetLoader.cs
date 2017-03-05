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
		INFLIGHT
	}

	public IEnumerator LoadAsset(string assetUrl, string assetName, int version) {
		WWW w = WWW.LoadFromCacheOrDownload(assetUrl,version);

		yield return w;

		if(!string.IsNullOrEmpty(w.error)) {
			Debug.Log("Asset load error: "+w.error);
		} else {
			Debug.Log("got the asset from: "+assetUrl);
			//Debug.Log(w.assetBundle.LoadAsset<GameObject>(assetName));
			string[] names = w.assetBundle.GetAllAssetNames();
			loadedAssets.Add(w.assetBundle.LoadAsset<GameObject>(names[0]));
			Instantiate(w.assetBundle.LoadAsset<GameObject>(names[0]), Vector3.zero, Quaternion.identity);
		}
	}


	public delegate void AssetLoadCallBack(GameObject g);

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

		//TODO fill the asset here using VO
    }

	public IEnumerator GetAsset(int assetId, PlaneAssetType planeType, AssetLoadCallBack loadCallback){
		List<GameObject> ret = loadedAssets.FindAll(x => x.GetComponent<AircraftCore>().aircraftId == assetId);

		if(planeType == PlaneAssetType.HANGAR){
			loadCallback(ret.Find(x => x.GetComponent<MovementModule>() == null));
		}else if(planeType == PlaneAssetType.INFLIGHT){
			loadCallback(ret.Find(x => x.GetComponent<MovementModule>() != null));
		}

		yield return null;
	}

	void Start () {
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else {
			Destroy(gameObject);
		}

		StartCoroutine(LoadAsset("http://ccsdacademy.com/mercwings/eurofighter/eurofighter.hangar","eurofighter.hangar",1));
	}

	public void PopulateLoadedAssetsWithData ()
	{
		foreach (GameObject asset in loadedAssets) {
			/*PlaneVO vo = AssetKeeper.instance.allPlanes.Find (delegate (DataClasses.PlaneVO obj) {
				return obj.id == asset.GetComponent<AircraftCore> ().aircraftId;
			});
			if (asset.GetComponent<MovementModule> () != null) {
				vo.ToPlane (asset);
			}*/
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
