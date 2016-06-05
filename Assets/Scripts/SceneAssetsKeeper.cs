using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneAssetsKeeper :MonoBehaviour {

	public static SceneAssetsKeeper instance;

	public List<GameObject> instantiatedAssets = new List<GameObject>();

	public List<GameObject> playerAssets = new List<GameObject>();

	public List<GameObject> opponentAssets = new List<GameObject>();

	public GameObject GetAssetById(int id){
		return instantiatedAssets.Find(delegate(GameObject obj) {
			return obj.GetComponent<AssetIdentifier>().sceneAssetId == id;	
		});
	}

	void Awake()
	{
		instance = this;
	}

}
