using UnityEngine;
using System.Collections;

public class SceneClickStateManager : MonoBehaviour {

	public enum ClickState {
		NO_SELECT_CLICK,
		SINGLE_SELECT_CLICK,
		MULTI_SELECT_CLICK
	}

	public static ClickState currentClickState = ClickState.NO_SELECT_CLICK;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
