using UnityEngine;
using System.Collections;

public class CombatSelectionHandler : MonoBehaviour {

	public SceneAssetsKeeper sceneAssetsKeeper;

	public static GameObject selectedObject;

	public void SelectRandomOtherObject(GameObject objectNotToChoose)
	{
		int rand = Random.Range(0,sceneAssetsKeeper.playerAssets.Count);

		if(sceneAssetsKeeper.playerAssets[rand] != objectNotToChoose)
		{
			selectedObject = sceneAssetsKeeper.playerAssets[rand];
		}
		else
		{
			SelectRandomOtherObject(objectNotToChoose);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
