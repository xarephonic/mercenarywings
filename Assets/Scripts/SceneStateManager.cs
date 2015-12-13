using UnityEngine;
using System.Collections;

public class SceneStateManager : MonoBehaviour {
	
	private int playedFrames;

	public enum  CombatSceneState{
		COMMAND,
		MOVEMENT
	}

	public static CombatSceneState currentState;

	public void ChangeState(CombatSceneState newState){

		playedFrames = 0;

		switch(newState){
		case CombatSceneState.COMMAND:
			currentState = newState;
			break;
		case CombatSceneState.MOVEMENT:
			currentState = newState;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(currentState == CombatSceneState.MOVEMENT){
			playedFrames ++;

			if(playedFrames == Constants.framesPerCombatRound)
				ChangeState(CombatSceneState.COMMAND);
		}
	}
}
