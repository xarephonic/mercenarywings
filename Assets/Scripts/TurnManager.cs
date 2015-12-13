using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnManager : MonoBehaviour {

	public static int currentTurn;

	public SceneStateManager stateMan;

	public Button endTurnButton;

	public void EndTurn(){
		currentTurn++;

		endTurnButton.interactable = false;

		stateMan.ChangeState(SceneStateManager.CombatSceneState.MOVEMENT);
	}

	public void CheckForEndTurnAvailability(){

		bool allAssetsRecievedOrders = SceneAssetsKeeper.sceneAssetsKeeper.playerAssets.TrueForAll(delegate(GameObject playerAsset) {
			return playerAsset.GetComponent<MovementModule>().commands.Count == TurnManager.currentTurn;
		});

		if(allAssetsRecievedOrders){
			endTurnButton.interactable = true;
		}
	}

	void OnLevelWasLoaded(){
		currentTurn = 1;
	}

	// Use this for initialization
	void Start () {
		currentTurn = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
