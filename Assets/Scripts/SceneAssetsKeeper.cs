using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneAssetsKeeper :MonoBehaviour {

	public static SceneAssetsKeeper instance;

	public List<GameObject> instantiatedAssets = new List<GameObject>();

	public List<GameObject> playerAssets = new List<GameObject>();

	public List<GameObject> opponentAssets = new List<GameObject>();

	void Awake()
	{
		instance = this;
	}

}
