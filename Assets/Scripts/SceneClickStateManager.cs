using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneClickStateManager : MonoBehaviour {

	public Text clickSelectModeText;

	public enum ClickState {
		NO_SELECT_CLICK,
		SINGLE_SELECT_CLICK,
		MULTI_SELECT_CLICK
	}

	public static ClickState currentClickState = ClickState.NO_SELECT_CLICK;

	public void SetClickState(ClickState newClickState){
		currentClickState = newClickState;
	}

	public void SetSingleSelectState(){
		currentClickState = ClickState.SINGLE_SELECT_CLICK;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(currentClickState > ClickState.NO_SELECT_CLICK){
			clickSelectModeText.gameObject.SetActive(true);
		}else{
			clickSelectModeText.gameObject.SetActive(false);
		}
	}
}
