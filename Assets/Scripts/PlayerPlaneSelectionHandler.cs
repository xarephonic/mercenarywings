using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerPlaneSelectionHandler : MonoBehaviour {

	public SceneAssetsKeeper sceneAssetsKeeper;

	public static GameObject selectedPlane;

	public GameObject previouslySelectedPlane;

	public UnityEvent OnSelectedPlaneChanged;

	public void ChangePlaneIndexBy(int change)
	{
		int currentIndex = sceneAssetsKeeper.playerAssets.FindIndex(delegate(GameObject item) {
			return item == selectedPlane;
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

		selectedPlane = sceneAssetsKeeper.playerAssets[currentIndex];
	}

	public void SwitchToNextPlaneWithoutCommands(){

		foreach(GameObject playerAsset in sceneAssetsKeeper.playerAssets){
			if(playerAsset.GetComponent<MovementModule>().commands.Count != TurnManager.currentTurn){
				selectedPlane = playerAsset;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(previouslySelectedPlane == null)
		{
			previouslySelectedPlane = selectedPlane;

			OnSelectedPlaneChanged.Invoke();
		}
		else if(previouslySelectedPlane != selectedPlane)
		{
			OnSelectedPlaneChanged.Invoke();

			previouslySelectedPlane = selectedPlane;
		}
	}
}
