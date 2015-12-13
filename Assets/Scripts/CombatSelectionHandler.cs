using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CombatSelectionHandler : MonoBehaviour {

	public SceneAssetsKeeper sceneAssetsKeeper;

	public static GameObject selectedObject;

	public GameObject oldSelectedObject;

	public UnityEvent OnSelectedObjectChanged;

	public void ChangePlaneIndexBy(int change)
	{
		int currentIndex = sceneAssetsKeeper.playerAssets.FindIndex(delegate(GameObject item) {
			return item == selectedObject;
		});

		currentIndex += change;

		if(currentIndex > sceneAssetsKeeper.playerAssets.Count-1)
		{
			currentIndex = 0;
		}
		else if(currentIndex < 0)
		{
			currentIndex = sceneAssetsKeeper.playerAssets.Count-1;
		}

		selectedObject = sceneAssetsKeeper.playerAssets[currentIndex];
	}

	public void SwitchToNextPlaneWithoutCommands(){

		foreach(GameObject playerAsset in sceneAssetsKeeper.playerAssets){
			if(playerAsset.GetComponent<MovementModule>().commands.Count != TurnManager.currentTurn){
				selectedObject = playerAsset;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(oldSelectedObject == null)
		{
			oldSelectedObject = selectedObject;

			OnSelectedObjectChanged.Invoke();
		}
		else if(oldSelectedObject != selectedObject)
		{
			OnSelectedObjectChanged.Invoke();

			oldSelectedObject = selectedObject;
		}
	}
}
