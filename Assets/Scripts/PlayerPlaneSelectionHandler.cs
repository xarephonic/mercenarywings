using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerPlaneSelectionHandler : MonoBehaviour {

	public SceneAssetsKeeper sceneAssetsKeeper;

	public static GameObject selectedPlane;

	public static void SetSelectedPlane(GameObject plane){
		PlayerPlaneSelectionHandler.selectedPlane = plane;
		OnSelectedPlaneChanged();
	}

	public GameObject previouslySelectedPlane;

	public delegate void PlaneSelectionAction();

	public static event PlaneSelectionAction OnSelectedPlaneChanged;

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
				SetSelectedPlane(playerAsset);
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		CombatMissionSetupHandler.OnMissionAssetsSpawned += SwitchToNextPlaneWithoutCommands;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
