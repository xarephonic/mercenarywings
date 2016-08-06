using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneAssetsKeeper :MonoBehaviour {

	public static SceneAssetsKeeper instance;

	public delegate void AssetDestructionAction(GameObject asset);

	public static event AssetDestructionAction OnAssetDestroyed;

	public List<GameObject> instantiatedAssets = new List<GameObject>();

	public List<GameObject> playerAssets = new List<GameObject>();

	public List<GameObject> opponentAssets = new List<GameObject>();

	public GameObject GetAssetById(int id){
		return instantiatedAssets.Find(delegate(GameObject obj) {
			return obj.GetComponent<AssetIdentifier>().sceneAssetId == id;	
		});
	}

	public void DestroyAsset(GameObject asset){
		//TODO do not destroy the asset itself, set it as inactive
		instantiatedAssets.Remove(asset);
		playerAssets.Remove(asset);
		opponentAssets.Remove(asset);

		OnAssetDestroyed(asset);

		Destroy(asset);

		//asset.SetActive(false);
	}

	void Awake()
	{
		instance = this;
	}

}
