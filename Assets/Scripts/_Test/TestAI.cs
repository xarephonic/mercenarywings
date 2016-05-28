using UnityEngine;
using System.Collections;

public class TestAI : MonoBehaviour {

	MovementModule myMovementModule;

	// Use this for initialization
	void Start () {
	
		myMovementModule = GetComponent<MovementModule>();
	}
	
	// Update is called once per frame
	void Update () {

		if(SceneStateManager.currentState == SceneStateManager.CombatSceneState.COMMAND){
			if(myMovementModule.commands.Count < TurnManager.currentTurn){
				myMovementModule.SetCommandsForThisTurn(Random.Range(-100,100),0,0,Random.Range(-100,100));
			}
		}
	}
}
