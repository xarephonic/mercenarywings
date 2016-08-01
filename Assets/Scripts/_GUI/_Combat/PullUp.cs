using UnityEngine;
using System.Collections;

public class PullUp : MonoBehaviour {

	public GameObject pullUpText;
	public GameObject fakePlane;

	string[] maskNames = new string[] {"Terrain", "Water"};

	void PullUpDetection(GameObject plane){
		Ray pullUpRay = new Ray(plane.transform.position,plane.transform.forward);
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(pullUpRay, out hit, plane.GetComponent<MovementModule>().airSpeed/3.6f, LayerMask.GetMask(maskNames) )){
			Debug.Log("pull up");

			ShowWarning(true);
		} else {
			ShowWarning(false);
		}
	}

	void ShowWarning(bool b){
		pullUpText.SetActive(b);
	}

	// Use this for initialization
	void Start () {

		fakePlane = GameObject.Find("FakePlane");

		PlayerPlaneSelectionHandler.OnSelectedPlaneChanged += PullUpDetection;
		SceneStateManager.OnSceneCombatStateChange += delegate(SceneStateManager.CombatSceneState newState) {
			if(newState == SceneStateManager.CombatSceneState.COMMAND){
				PullUpDetection(PlayerPlaneSelectionHandler.selectedPlane);
			}
		};

		//TODO move this to collision warning logic
		JoystickControl.OnJoystickValueChanged += delegate {
			Ray pullUpRay = new Ray(fakePlane.transform.position,Vector3.up);

			if(Physics.Raycast(pullUpRay, LayerMask.GetMask(maskNames))){
				ShowWarning(true);
			}else {
				ShowWarning(false);
			}
		};

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
